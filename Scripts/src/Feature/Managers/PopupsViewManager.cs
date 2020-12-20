using System;
using Core.src.Messaging;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using UnityEngine;
using Zenject;

namespace Scripts.src.Feature.Managers
{
    public class PopupsViewManager : MonoBehaviour, IPopupsViewManager<PopupEntityBase>
    {
#pragma warning disable 0649
        
        [Inject]
        private DiContainer container;

        [Inject]
        private IEventBus eventBus;
        
        [Inject]
        private IPopupsManager popupsManager;
        
        private GameObject popupsCanvas;
        
        [Inject]
        private void Initialize([Inject(Id = "PopupsCanvas")] GameObject popupsCanvasPrefab)
        {
            popupsCanvas = container.InstantiatePrefab(popupsCanvasPrefab);
        }
        
#pragma warning restore

        public PopupViewBase CurrentOpenedPopup => popupsManager.GetCurrentOpenedPopup();

        public bool IsAnyOpenedPopups => CurrentOpenedPopup != null;

        public async void Open(PopupEntityBase popupData, Action<PopupViewBase> onOpened = null, Action onFail = null)
        {
            var request = new LoadPopupAssetRequest()
            {
                AssetId = popupData.PopupId,
            };

            var canBePopupOpened = popupsManager.CanPopupBeOpened(popupData.RulesToOpen);
            if (canBePopupOpened)
            {
                var response = await eventBus.FireAsync<LoadPopupAssetRequest, LoadPopupAssetResponse>(request);
                var isError = response.Exception != null;
                if (isError)
                {
                    OnPopupLoadedFailHandler(response.Exception);
                }
                else
                {
                    OnPopupLoadedSuccessHandler(response.Result, popupData, onOpened);
                } 
            }
            else
            {
                popupsManager.EnqueuePopup(popupData, onOpened, onFail);
            }
        }

        public void Close(PopupEntityBase popupData, Action onClosed, Action onFail)
        {
            var expectedPopup = popupsManager.GetVisiblePopup(popupData);
            if (expectedPopup == null)
            {
                Debug.LogError($"Popup with id {popupData.PopupId} can't be found in visiblePopups");
                onFail();
                return;
            }
            expectedPopup.OnHided += (popup)=> onClosed?.Invoke();
            expectedPopup.Hide();
        }

        private void Close(PopupViewBase popup)
        {
            popupsManager.Remove(popup);
            Destroy(popup.gameObject);
            var queuedPopup = popupsManager.DequeuePopup();
            if (queuedPopup != null)
            {
                Open(queuedPopup.PopupData, queuedPopup.OnOpened, queuedPopup.OnFail);
            }
        }

        private void OnPopupLoadedSuccessHandler(PopupViewBase popupView, PopupEntityBase popupData, Action<PopupViewBase> onSuccess)
        {
            var instantiatePrefab = container.InstantiatePrefab(popupView);
            var instantiatedPopupView = instantiatePrefab.GetComponent<PopupViewBase>();
            instantiatedPopupView.OnHided += Close;
            instantiatedPopupView.Data = popupData;
            instantiatedPopupView.SetData(popupData);
            instantiatedPopupView.Show();
            instantiatePrefab.transform.SetParent(popupsCanvas.transform, false);
            onSuccess?.Invoke(instantiatedPopupView);

            popupsManager.AddPopupAsVisible(instantiatedPopupView);
        }  
        
        private void OnPopupLoadedFailHandler(Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }
}
