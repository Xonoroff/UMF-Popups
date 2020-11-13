using Scripts.src.Feature.Entities;
using UnityEditor;
using UnityEngine;

namespace Scripts.src.Editor
{
    public class PopupsModuleConfigCreator : MonoBehaviour
    {
        [MenuItem("Modular framework/Create/Popups module config")]
        public static void CreateMyAsset()
        {
            PopupsModuleConfig asset = ScriptableObject.CreateInstance<PopupsModuleConfig>();

            AssetDatabase.CreateAsset(asset, $"Assets/Resources/{ PopupsModuleConfig.FILE_NAME }.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
