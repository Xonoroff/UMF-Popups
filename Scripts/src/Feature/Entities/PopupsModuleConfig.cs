using UnityEngine;

namespace Scripts.src.Feature.Entities
{
    public class PopupsModuleConfig : ScriptableObject
    {
        public const string FILE_NAME = "PopupsSystemModuleConfig";
        
#pragma warning disable 0649
        
        [SerializeField]
        private GameObject canvas;
        
#pragma warning restore
        
        public GameObject GetCanvas()
        {
            return canvas;
        }
    }
}
