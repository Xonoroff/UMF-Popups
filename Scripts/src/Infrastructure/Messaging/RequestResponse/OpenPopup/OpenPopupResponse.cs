using System;
using PopupsModule.src.Infrastructure.Entities;

namespace PopupsModule.src.Infrastructure.Messaging.RequestResponse.OpenPopup
{
    public class OpenPopupResponse
    {
        public PopupViewBase OpenedPopup { get; set; }
        
        public Exception Exception { get; set; }

        public OpenPopupResponse(PopupViewBase popup, Exception exception = null)
        {
            if (exception != null)
            {
                Exception = exception;
            }
            
            if (popup != null)
            {
                OpenedPopup = popup;
            }
            else if(exception != null)
            {
                Exception = new NullReferenceException();
            }
        }
    }
}