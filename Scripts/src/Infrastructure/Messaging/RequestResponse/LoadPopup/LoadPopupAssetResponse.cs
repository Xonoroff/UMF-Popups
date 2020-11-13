using System;
using PopupsModule.src.Infrastructure.Entities;

namespace PopupsModule.src.Infrastructure.Messaging.RequestResponse.LoadPopup
{
  public class LoadPopupAssetResponse
  {
    public LoadPopupAssetResponse(object result, Exception exception = null)
    {
      this.Exception = exception;
      if (result != null && exception == null)
      {
        this.Result = result as PopupViewBase;
        if (this.Result == null)
        {
          this.Exception =
            new InvalidCastException(
              $"Can't cast {result.GetType()} to {nameof(PopupViewBase)} in {nameof(LoadPopupAssetResponse)}");
        }
      }
    }
    
    public PopupViewBase Result { get; set; }
  
    public Exception Exception { get; set; }
  }
}
