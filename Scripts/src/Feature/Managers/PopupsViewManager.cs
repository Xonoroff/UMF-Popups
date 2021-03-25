using System;
using System.Collections.Generic;
using Core.src.Messaging;
using Core.src.Utils;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using Scripts.src.Feature.Entities;
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
        
        private GameObject projectPopupsCanvas;
        
        private GameObject _scenePopupsCanvas;
        
        private GameObject scenePopupsCanvas
        {
            get
            {
                if (_scenePopupsCanvas == null)
                {
                    var root = new GameObject() {name = "PopupsSceneRoot"};
                    _scenePopupsCanvas = Instantiate(projectPopupsCanvas, root.transform);
                }

                return _scenePopupsCanvas;
            }
        }
        
        [Inject]
        private void Initialize([Inject(Id = "PopupsCanvas")] GameObject popupsCanvasPrefab)
        {
            this.projectPopupsCanvas = container.InstantiatePrefab(popupsCanvasPrefab);
        }
        
#pragma warning restore

        public PopupViewBase CurrentOpenedPopup => popupsManager.GetCurrentOpenedPopup();

        public bool IsAnyOpenedPopups => CurrentOpenedPopup != null;

        public async void Open(PopupEntityBase popupData, Action<PopupViewBase> onOpened = null, Action onFail = null)
        {
            var rulesToOpen = container.TryResolveId<List<PopupRuleBase>>(popupData.PopupOpenRule);
            var canBePopupOpened = popupsManager.CanPopupBeOpened(rulesToOpen);
            if (canBePopupOpened)
            {
                PopupViewBase view = null;
                if (popupData is PopupEntityWithId entityWithId)
                {
                    var request = new LoadPopupAssetRequest()
                    {
                        AssetId = entityWithId.PopupId,
                    };
                    var response = await eventBus.FireAsync<LoadPopupAssetRequest, LoadPopupAssetResponse>(request);
                    var isError = response.Exception != null;
                    if (isError)
                    {
                        OnPopupLoadedFailHandler(response.Exception);
                    }
                    else
                    {
                        view = response.Result;
                    }
                }
                else if (popupData is PopupEntityWithAssetPrefab entityWithPrefab)
                {
                    view = entityWithPrefab.PopupViewPrefab;
                }

                OnPopupLoadedSuccessHandler(view, popupData, onOpened);
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
                Debug.LogError($"Popup with id {popupData.Id} can't be found in visiblePopups");
                onFail();
                return;
            }
            expectedPopup.OnHided += (popup)=> onClosed?.Invoke();
            expectedPopup.Hide();
        }

        private void Close(PopupViewBase popup)
        {
            popupsManager.Remove(popup);
            if (popup != null)
            {
                Destroy(popup.gameObject);
            }
            
            var queuedPopup = popupsManager.DequeuePopup();
            if (queuedPopup != null)
            {
                Open(queuedPopup.PopupData, queuedPopup.OnOpened, queuedPopup.OnFail);
            }
        }

        private void OnPopupLoadedSuccessHandler(PopupViewBase popupView, PopupEntityBase popupData, Action<PopupViewBase> onSuccess)
        {
            var instantiatePrefab = container.InstantiatePrefab(popupView);
            var eventWrapperComponent = instantiatePrefab.AddComponent<MonoBehaviourEventWrapperComponent>();
            var instantiatedPopupView = instantiatePrefab.GetComponent<PopupViewBase>();
            eventWrapperComponent.OnDestroyEvent += CloseHandlerWrapper;
            instantiatedPopupView.OnHided += (p)=>
            {
                eventWrapperComponent.OnDestroyEvent -= CloseHandlerWrapper;
                CloseHandlerWrapper();
            };

            void CloseHandlerWrapper()
            {
                Close(instantiatedPopupView);
            }
            
            instantiatedPopupView.Data = popupData;
            instantiatedPopupView.SetData(popupData);
            instantiatedPopupView.Show();
            var canvas = GetCanvasTransform(popupData.CanvasType);
            instantiatePrefab.transform.SetParent(canvas, false);
            onSuccess?.Invoke(instantiatedPopupView);

            popupsManager.AddPopupAsVisible(instantiatedPopupView);
        }  
        
        private void OnPopupLoadedFailHandler(Exception exception)
        {
            Debug.LogError(exception.Message);
        }

        private Transform GetCanvasTransform(PopupsCanvasType canvas)
        {
            if (canvas == PopupsCanvasType.Project)
            {
                return projectPopupsCanvas.transform;
            }

            return scenePopupsCanvas.transform;
        }
    }
}