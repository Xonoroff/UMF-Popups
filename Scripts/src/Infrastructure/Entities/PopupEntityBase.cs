using Core.src.Entity;

namespace PopupsModule.src.Infrastructure.Entities
{
    public class PopupEntityBase : BaseEntity
    {
        public string PopupId { get; set; }
        
        public int Order { get; set; }
        
        public object PopupData { get; set; }
    }
}