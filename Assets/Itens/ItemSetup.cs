using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using TMPro;

namespace Game.Item
{
    [System.Serializable]
    public class ItemSetup 
    {
        public ItemType Type;
        [SerializeField]
        private SOInt soInt;
        [SerializeField]
        private TextMeshProUGUI uiText;

        public int Value { 
            get => soInt.value; 
            set{ 
                soInt.value = value;
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            if (uiText)
                uiText.text = Value.ToString("00");
        }
    }
}