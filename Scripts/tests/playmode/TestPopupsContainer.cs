using System.Collections.Generic;
using PopupsModule.src.Infrastructure.Entities;
using UnityEngine;

namespace Scripts.tests.playmode
{
    public class TestPopupsContainer : MonoBehaviour
    {
#pragma warning disable 0649
        
        [SerializeField]
        private List<PopupViewBase> popups;

#pragma warning restore
        
        public List<PopupViewBase> GetPopups()
        {
            return popups;
        }
    }
}
