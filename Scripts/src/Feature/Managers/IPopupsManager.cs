using System;
using PopupsModule.src.Infrastructure.Entities;

namespace PopupsModule.src.Feature.Managers
{
    public interface IPopupsManager
    {
        void Remove(PopupViewBase popup);
        PopupViewBase GetCurrentOpenedPopup();
        bool CanPopupBeOpened(PopupEntityBase popupData);
        void EnqueuePopup(PopupEntityBase popupData, Action<PopupViewBase> onOpened = null, Action onFail = null);
        void AddPopupAsVisible(PopupViewBase instantiatedPopupView);
        PopupViewBase GetVisiblePopup(PopupEntityBase popupData);
        QueuedPopup DequeuePopup();
    }
}