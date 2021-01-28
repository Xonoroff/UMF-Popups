using System.Collections.Generic;
using Core.src.Entity;
using Scripts.src.Feature.Entities;
using Scripts.src.Infrastructure.Rules;

namespace PopupsModule.src.Infrastructure.Entities
{
    public abstract class PopupEntityBase : BaseEntity
    {
        public int Order { get; set; }
        
        public object PopupData { get; set; }

        public string PopupOpenRule { get; }
        
        public PopupEntityBase(string ruleId = PopupRuleKeys.ShowIfNoVisiblePopupsRule)
        {
            PopupOpenRule = ruleId;
        }
    }
}