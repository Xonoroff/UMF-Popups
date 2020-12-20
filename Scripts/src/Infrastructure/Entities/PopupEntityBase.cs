using System.Collections.Generic;
using Core.src.Entity;
using Scripts.src.Feature.Entities;
using Zenject;

namespace PopupsModule.src.Infrastructure.Entities
{
    public class PopupEntityBase : BaseEntity
    {
        public string PopupId { get; set; }
        
        public int Order { get; set; }
        
        public object PopupData { get; set; }

        public List<PopupOpenRuleBase> RulesToOpen { get; set; }

        public PopupEntityBase(List<PopupOpenRuleBase> rulesToOpen = null)
        {
            RulesToOpen = rulesToOpen;
        }
    }
}