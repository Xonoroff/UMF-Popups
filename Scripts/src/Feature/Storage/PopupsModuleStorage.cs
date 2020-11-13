using System.Collections.Generic;
using System.Linq;
using PopupsModule.src.Infrastructure.Entities;

namespace Scripts.src.Feature.Storage
{
    public class PopupsModuleStorage : IPopupsModuleStorage
    {
        private readonly List<PopupViewBase> visiblePopups = new List<PopupViewBase>();
        private readonly Queue<QueuedPopup> queuedPopups = new Queue<QueuedPopup>();
        
        public PopupViewBase GetCurrentOpenedPopup()
        {
            return visiblePopups.LastOrDefault();
        }

        public bool IsAnyVisiblePopups()
        {
            return GetCurrentOpenedPopup() == null && visiblePopups.Count == 0;
        }

        public void AddPopupToQueue(QueuedPopup queuedPopup)
        {
            queuedPopups.Enqueue(queuedPopup);
        }

        public void RemoveVisiblePopup(PopupViewBase popupView)
        {
            var popup = visiblePopups.First(x => x == popupView);
            visiblePopups.Remove(popup);
        }

        public void AddVisiblePopup(PopupViewBase instantiatedPopupView)
        {
            visiblePopups.Add(instantiatedPopupView);
        }

        public PopupViewBase GetVisiblePopup(PopupEntityBase popupData)
        {
            return visiblePopups.FirstOrDefault(x => x.Data == popupData);
        }

        public QueuedPopup Dequeue()
        {
            return queuedPopups.Dequeue();
        }
    }
}