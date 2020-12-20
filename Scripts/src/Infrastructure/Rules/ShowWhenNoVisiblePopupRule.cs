using PopupsModule.src.Infrastructure.Entities;
using PopupsModule.src.Infrastructure.Interfaces;
using Scripts.src.Feature.Entities;

namespace Scripts.src.Feature.Rules
{
    public class ShowWhenNoVisiblePopupRule : PopupRuleBase
    {
        private readonly IPopupsViewManager<PopupEntityBase> viewManager;

        public ShowWhenNoVisiblePopupRule(IPopupsViewManager<PopupEntityBase> viewManager)
        {
            this.viewManager = viewManager;
        }
        
        public override bool CanBeOpened()
        {
            return viewManager.CurrentOpenedPopup == null;
        }
    }
}