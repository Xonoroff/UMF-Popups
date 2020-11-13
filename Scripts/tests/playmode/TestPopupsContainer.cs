using System.Collections.Generic;
using PopupsModule.src.Infrastructure.Entities;
using UnityEngine;

namespace Scripts.tests.playmode
{
    public class TestPopupsContainer : MonoBehaviour
    {
        [SerializeField]
        private List<PopupViewBase> popups;

        public List<PopupViewBase> GetPopups()
        {
            return popups;
        }
    }
}
