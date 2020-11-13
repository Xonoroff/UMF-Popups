using Zenject;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using Scripts.tests.playmode;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using Assert = NUnit.Framework.Assert;

public class PopupsModulePlaymodeTests : SceneTestFixture
{
    [Inject]
    private IPopupsViewManager<PopupEntityBase> popupsViewManager;

    private PopupViewBase cachedPopup;
    private List<PopupViewBase> cachedPopups;
    
    private void OnAssetShouldBeLoadedCallback(LoadPopupAssetRequest request)
    {
        var result = cachedPopups.FirstOrDefault(x => x.gameObject.name == request.AssetId);
        var response = new LoadPopupAssetResponse(result);
        request.Callback(response);
    }

    [UnityTest]
    public IEnumerator TestScene()
    {
        yield return LoadScene("PopupsModule.Tests.Playmode.MainScene");

        var signalBus = SceneContainer.Resolve<SignalBus>();
        popupsViewManager = SceneContainer.Resolve<IPopupsViewManager<PopupEntityBase>>();
        signalBus.Subscribe<LoadPopupAssetRequest>(OnAssetShouldBeLoadedCallback);
        var containerWithPopups = GameObject.FindObjectOfType<TestPopupsContainer>();
        cachedPopups = containerWithPopups.GetPopups();
        
        for (int i = 0; i < cachedPopups.Count; i++)
        {
            var popupData = new PopupEntityBase()
            {
                PopupData = null,
                PopupId = cachedPopups[i].gameObject.name
            };   
            
            popupsViewManager.Open(popupData, OnSuccess, OnFail);
        }
        
        yield return new WaitForSeconds(1f);
        cachedPopup.Hide();

        yield return new WaitForSeconds(1f);
        cachedPopup.Hide();
        
        yield return new WaitForSeconds(1f);
        cachedPopup.Hide();
        
        yield return new WaitForSeconds(5);
        // TODO: Add assertions here now that the scene has started
        // Or you can just uncomment to simply wait some time to make sure the scene plays without errors
        //yield return new WaitForSeconds(1.0f);

        // Note that you can use SceneContainer.Resolve to look up objects that you need for assertions
    }
    

    protected void OnFail()
    {
        Debug.Log("Failed to open popup");
    }

    protected void OnSuccess(PopupViewBase openedPopup)
    {
        cachedPopup = openedPopup;
        Debug.Log($"Popup has been opened { openedPopup.gameObject.name }");
    }
}