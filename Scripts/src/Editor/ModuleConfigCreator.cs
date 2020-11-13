using Scripts.src.Feature.Entities;
using UnityEditor;
using UnityEngine;

namespace Scripts.src.Editor
{
    public class ModuleConfigCreator : MonoBehaviour
    {
        [MenuItem("Modular framework/Create/Popups module config")]
        public static void CreateMyAsset()
        {
            PopupsSystemModuleConfig asset = ScriptableObject.CreateInstance<PopupsSystemModuleConfig>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/PopupsSystemModuleConfig.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}
