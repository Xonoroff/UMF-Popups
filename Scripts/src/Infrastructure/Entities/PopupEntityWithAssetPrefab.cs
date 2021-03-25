using Scripts.src.Infrastructure.Rules;

namespace PopupsModule.src.Infrastructure.Entities
{
    public class PopupEntityWithAssetPrefab : PopupEntityBase
    {
        public PopupViewBase PopupViewPrefab { get; private set; }

        public PopupEntityWithAssetPrefab(PopupViewBase popupViewPrefab, string ruleId = PopupRuleKeys.ShowIfNoVisiblePopupsRule, PopupsCanvasType canvasType = PopupsCanvasType.Scene) : base(ruleId, canvasType)
        {
            PopupViewPrefab = popupViewPrefab;
        }
    }
}