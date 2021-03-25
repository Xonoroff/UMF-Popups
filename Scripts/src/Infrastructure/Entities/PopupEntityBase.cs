using Core.src.Entity;
using Scripts.src.Infrastructure.Rules;

namespace PopupsModule.src.Infrastructure.Entities
{
    public abstract class PopupEntityBase : BaseEntity
    {
        public int Order { get; set; }
        
        public object PopupData { get; set; }

        public string PopupOpenRule { get; }
        
        public PopupsCanvasType CanvasType { get; }
        
        public PopupEntityBase(string ruleId = PopupRuleKeys.ShowIfNoVisiblePopupsRule, PopupsCanvasType canvasType = PopupsCanvasType.Scene)
        {
            PopupOpenRule = ruleId;
            CanvasType = canvasType;
        }
    }
}