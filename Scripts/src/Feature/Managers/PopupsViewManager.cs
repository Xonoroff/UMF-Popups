using System;
using System.Collections.Generic;
using System.Linq;
using Core.src.Messaging;
using Core.src.Utils;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using UnityEngine;
using Zenject;

namespace PopupsModule.src.Feature.Managers
{
    public class PopupsViewManager : MonoBehaviour, IPopupsViewManager<PopupEntityBase>
    {
        [Inject]
        private DiContainer container;

        [Inject]
        private IEventBus eventBus;
        
        private GameObject popupsCanvas;

        private List<PopupViewBase> visiblePopups = new List<PopupViewBase>();

        private PopupViewBase currentOpenedPopup;

        [Inject]
        private void Initialize([Inject(Id = "PopupsCanvas")] GameObject popupsCanvasPrefab)
        {
            popupsCanvas = container.InstantiatePrefab(popupsCanvasPrefab);
        }

        public PopupViewBase CurrentOpenedPopup => currentOpenedPopup;

        public bool IsAnyOpenedPopups => currentOpenedPopup != null;

        public async void Open(PopupEntityBase popupData, Action<PopupViewBase> onOpened = null, Action onFail = null)
        {
            var request = new LoadPopupAssetRequest()
            {
                AssetId = popupData.PopupId,
            };
            
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

        public void Close(PopupEntityBase popupData, Action onClosed, Action onFail)
        {
            var expectedPopup = visiblePopups.FirstOrDefault(x => x.Data == popupData);
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
            visiblePopups.Remove(popup);
            currentOpenedPopup = null;
            Destroy(popup.gameObject);
        }

        private void OnPopupLoadedSuccessHandler(PopupViewBase popupView, PopupEntityBase popupData, Action<PopupViewBase> onSuccess)
        {
            var instantiatePrefab = container.InstantiatePrefab(popupView);
            var instantiatedPopupView = instantiatePrefab.GetComponent<PopupViewBase>();
            instantiatedPopupView.OnHided += Close;
            instantiatedPopupView.Data = popupData;
            instantiatedPopupView.SetData(popupData);
            instantiatedPopupView.Show();
            visiblePopups.Add(instantiatedPopupView);
            instantiatePrefab.transform.SetParent(popupsCanvas.transform, false);
            onSuccess?.Invoke(instantiatedPopupView);
            currentOpenedPopup = instantiatedPopupView;
        }  
        
        private void OnPopupLoadedFailHandler(Exception exception)
        {
            
        }
    }
}
