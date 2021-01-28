using Scripts.src.Infrastructure.Rules;

namespace PopupsModule.src.Infrastructure.Entities
{
    public class PopupEntityWithId : PopupEntityBase
    {
        public string PopupId { get; private set; }

        public PopupEntityWithId(string popupId, string ruleId = PopupRuleKeys.ShowIfNoVisiblePopupsRule) : base(ruleId)
        {
            PopupId = popupId;
        }
    }
}