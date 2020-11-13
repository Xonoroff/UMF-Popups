using Core.src.Utils;
using PopupsModule.src.Feature.Managers;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.OpenPopup;
using Zenject;

namespace Scripts.src.Feature.Installers
{
    public class PopupsModuleGlobalInstaller : GlobalInstallerBase<PopupsModuleGlobalInstaller, PopupsModuleInstaller>
    {
        protected override string SubContainerName => "PopupsModuleContainer";

#pragma warning disable 0649
        
        [Inject]
        private SignalBus signalBus;
        
#pragma warning restore

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