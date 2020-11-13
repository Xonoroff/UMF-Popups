using Core.src.Messaging;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.OpenPopup;
using Zenject;

namespace Scripts.src.Feature.Installers
{
    public class PopupsModuleSignalsInstaller : Installer<PopupsModuleSignalsInstaller>
    {
#pragma warning disable 0649
        
        [Inject]
        private IEventBus eventBus;
        
#pragma warning restore
        
        public override void InstallBindings()
        {
            eventBus.Subscribe<OpenPopupRequest>((request) =>
            {
                var viewManager = Container.Resolve<IPopupsViewManager<PopupEntityBase>>();
                viewManager.Open(request.popupData, (popup) =>
                {
                    var response = new OpenPopupResponse(popup);
                    request.Callback(response);
                }, () =>
                {
                    var response = new OpenPopupResponse(null);
                    request.Callback(response);
                });
            });
        }
    }
}