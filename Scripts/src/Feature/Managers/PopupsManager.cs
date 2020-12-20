using System;
using System.Collections.Generic;
using System.Linq;
using PopupsModule.src.Infrastructure.Entities;
using Scripts.src.Feature.Entities;
using Scripts.src.Feature.Storage;

namespace Scripts.src.Feature.Managers
{
    public class PopupsManager : IPopupsManager
    {
        private readonly IPopupsModuleStorage popupsModuleStorage;
        
        public PopupsManager(IPopupsModuleStorage moduleStorage)
        {
            popupsModuleStorage = moduleStorage;
        }
        
        public void Remove(PopupViewBase popup)
        {
            popupsModuleStorage.RemoveVisiblePopup(popup);
        }

        public PopupViewBase GetCurrentOpenedPopup()
        {
            return popupsModuleStorage.GetCurrentOpenedPopup();
        }

        public bool CanPopupBeOpened(List<PopupOpenRuleBase> rules)
        {
            if (rules == null)
            {
                return true;
            }

            if (rules.Count == 0)
            {
                return true;
            }

            return rules.All(x => x.CanBeOpened());
        }

        public void EnqueuePopup(PopupEntityBase popupData, Action<PopupViewBase> onOpened = null, Action onFail = null)
        {
            var queuedPopup = new QueuedPopup(popupData, onOpened, onFail);
            popupsModuleStorage.AddPopupToQueue(queuedPopup);
        }

        public void AddPopupAsVisible(PopupViewBase instantiatedPopupView)
        {
            popupsModuleStorage.AddVisiblePopup(instantiatedPopupView);
        }

        public PopupViewBase GetVisiblePopup(PopupEntityBase popupData)
        {
            return popupsModuleStorage.GetVisiblePopup(popupData);
        }

        public QueuedPopup DequeuePopup()
        {
            return popupsModuleStorage.DequeuePopup();
        }
    }
}