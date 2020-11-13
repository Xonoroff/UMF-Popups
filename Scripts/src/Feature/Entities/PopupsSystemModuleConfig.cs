using UnityEngine;

namespace Scripts.src.Feature.Entities
{
    public class PopupsSystemModuleConfig : ScriptableObject
    {
        [SerializeField]
        private GameObject canvas;

        public GameObject GetCanvas()
        {
            return canvas;
        }
    }
}
