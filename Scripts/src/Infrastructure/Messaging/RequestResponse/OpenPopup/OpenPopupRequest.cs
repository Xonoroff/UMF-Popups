using Core.src.Messaging;
using Core.src.Utils;
using PopupsModule.src.Infrastructure.Entities;

namespace PopupsModule.src.Infrastructure.Messaging.RequestResponse.OpenPopup
{
    public class OpenPopupRequest : EventBusRequest<OpenPopupResponse>
    {
        public PopupEntityBase popupData { get; set; }
    }
}