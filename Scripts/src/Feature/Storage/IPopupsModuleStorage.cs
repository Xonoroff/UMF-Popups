using PopupsModule.src.Infrastructure.Entities;

namespace Scripts.src.Feature.Storage
{
    public interface IPopupsModuleStorage
    {
        PopupViewBase GetCurrentOpenedPopup();
        bool IsAnyVisiblePopups();
        void AddPopupToQueue(QueuedPopup queuedPopup);
        void RemoveVisiblePopup(PopupViewBase popupView);
        void AddVisiblePopup(PopupViewBase instantiatedPopupView);
        PopupViewBase GetVisiblePopup(PopupEntityBase popupData);
        QueuedPopup Dequeue();
    }
}