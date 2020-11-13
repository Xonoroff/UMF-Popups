using Core.src.Utils;
using PopupsModule.src.Feature.Managers;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.OpenPopup;
using UnityEngine;
using Zenject;

namespace PopupsModule.src.Feature.Installers
{
    public class PopupsModuleGlobalInstaller : GlobalInstallerBase<PopupsModuleGlobalInstaller, PopupsModuleInstaller>
    {
        protected override string SubContainerName => "PopupsModuleContainer";
        
        [Inject]
        private SignalBus signalBus;
        
        public override void InstallBindings()
        {
            signalBus.DeclareSignal<LoadPopupAssetRequest>();
            signalBus.DeclareSignal<OpenPopupRequest>();
            
            base.InstallBindings();
            
            Container.Bind<IPopupsViewManager<PopupEntityBase>>()
                .To<PopupsViewManager>()
                .FromSubContainerResolve()
                .ByInstanceGetter(SubContainerInstanceGetter)
                .AsCached();
            
            PopupsModuleSignalsInstaller.Install(Container);
        }
    }
}