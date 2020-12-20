using Scripts.src.Feature.Entities;

namespace Scripts.src.Infrastructure.Rules
{
    public class ForceShowPopupRule : PopupRuleBase
    {
        public override bool CanBeOpened()
        {
            return true;
        }
    }
}