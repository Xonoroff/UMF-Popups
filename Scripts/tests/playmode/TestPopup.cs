using PopupsModule.src.Infrastructure.Entities;
using UnityEngine;

namespace Scripts.tests.playmode
{
    public class TestPopup : PopupViewBase
    {
        public override void SetData(PopupEntityBase popupData)
        {
            Debug.Log($"Popup set data {gameObject.name}");
        }

        public override void Show()
        {
            Debug.Log($"Popup shown {gameObject.name}");
            OnShown?.Invoke(this);
        }

        public override void Hide()
        {
            Debug.Log($"Popup hidden {gameObject.name}");
            OnHided?.Invoke(this);
        }
    }
}
