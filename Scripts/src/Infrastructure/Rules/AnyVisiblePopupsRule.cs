using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using Scripts.src.Feature.Entities;

namespace Scripts.src.Feature.Rules
{
    public class AnyVisiblePopupsRule : PopupOpenRuleBase
    {
        private readonly IPopupsViewManager<PopupEntityBase> viewManager;

        public AnyVisiblePopupsRule(IPopupsViewManager<PopupEntityBase> viewManager)
        {
            this.viewManager = viewManager;
        }
        
        public override bool CanBeOpened()
        {
            return viewManager.CurrentOpenedPopup == null;
        }
    }
}