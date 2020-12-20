using System.Collections.Generic;
using Core.src.Utils;
using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup;
using PopupsModule.src.Infrastructure.Messaging.RequestResponse.OpenPopup;
using Scripts.src.Feature.Entities;
using Scripts.src.Feature.Managers;
using Scripts.src.Feature.Rules;
using Scripts.src.Infrastructure.Rules;
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

            Container.Bind<List<PopupRuleBase>>().WithId(PopupRuleKeys.ShowIfNoVisiblePopupsRule).FromMethod(x =>
            {
                var defaultRuleset = new List<PopupRuleBase>();
                var defaultRule = Container.Resolve<ShowWhenNoVisiblePopupRule>();
                defaultRuleset.Add(defaultRule);
                return defaultRuleset;
            });
            
            Container.Bind<List<PopupRuleBase>>().WithId(PopupRuleKeys.ForceShowRule).FromMethod(x =>
            {
                var defaultRuleset = new List<PopupRuleBase>();
                var defaultRule = Container.Resolve<ForceShowPopupRule>();
                defaultRuleset.Add(defaultRule);
                return defaultRuleset;
            });
                        
            Container.Bind<ShowWhenNoVisiblePopupRule>().AsTransient();
            Container.Bind<ForceShowPopupRule>().AsTransient();

            PopupsModuleSignalsInstaller.Install(Container);
        }
    }
}