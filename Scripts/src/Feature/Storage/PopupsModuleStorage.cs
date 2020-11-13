using System.Collections.Generic;
using System.Linq;
using PopupsModule.src.Infrastructure.Entities;

namespace Scripts.src.Feature.Storage
{
    public class PopupsModuleStorage : IPopupsModuleStorage
    {
        private readonly List<PopupViewBase> visiblePopups = new List<PopupViewBase>();
        private readonly List<QueuedPopup> queuedPopups = new List<QueuedPopup>();
        
        public PopupViewBase GetCurrentOpenedPopup()
        {
            return visiblePopups.LastOrDefault();
        }

        public bool IsAnyVisiblePopups()
        {
            return visiblePopups.Count != 0;
        }

        public void AddPopupToQueue(QueuedPopup queuedPopup)
        {
            queuedPopups.Add(queuedPopup);
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

        public QueuedPopup DequeuePopup()
        {
            var popupInQueue = queuedPopups.OrderBy(x=>x.PopupData.Order).FirstOrDefault();
            queuedPopups.Remove(popupInQueue);
            return popupInQueue;
        }
    }
}