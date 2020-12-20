using System;
using System.Collections.Generic;
using PopupsModule.src.Infrastructure.Entities;
using Scripts.src.Feature.Entities;

namespace Scripts.src.Feature.Managers
{
    public interface IPopupsManager
    {
        void Remove(PopupViewBase popup);
        PopupViewBase GetCurrentOpenedPopup();
        bool CanPopupBeOpened(List<PopupRuleBase> rules);
        void EnqueuePopup(PopupEntityBase popupData, Action<PopupViewBase> onOpened = null, Action onFail = null);
        void AddPopupAsVisible(PopupViewBase instantiatedPopupView);
        PopupViewBase GetVisiblePopup(PopupEntityBase popupData);
        QueuedPopup DequeuePopup();
    }
}