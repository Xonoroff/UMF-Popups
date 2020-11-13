using Core.src.Messaging;
using Core.src.Utils;

namespace PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup
{
    public class LoadPopupAssetRequest : EventBusRequest<LoadPopupAssetResponse>
    {
        public string AssetId;
    }
}
