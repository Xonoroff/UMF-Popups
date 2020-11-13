using System;
using UnityEngine;

namespace PopupsModule.src.Infrastructure.Entities
{
    public abstract class PopupViewBase : MonoBehaviour
    {
        public Action<PopupViewBase> OnShown { get; set; }
        
        public Action<PopupViewBase> OnHided { get; set; }
        
        public abstract void SetData(PopupEntityBase popupData);

        public abstract void Show();
        
        public PopupEntityBase Data { get; set; }
        
        public abstract void Hide();

        public void Reset()
        {
            OnShown = default;
            OnHided = default;
            Data = null;
        }
    }
}
