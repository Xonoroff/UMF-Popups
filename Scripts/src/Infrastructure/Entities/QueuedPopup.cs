using System;

namespace PopupsModule.src.Infrastructure.Entities
{
    public class QueuedPopup
    {
        public PopupEntityBase PopupData { get; private set; }
        public Action<PopupViewBase> OnOpened { get; private set; }
        public Action OnFail { get; private set; }

        public QueuedPopup(PopupEntityBase popupData, Action<PopupViewBase> onOpened, Action onFail)
        {
            PopupData = popupData;
            OnOpened = onOpened;
            OnFail = onFail;
        }
    }
}