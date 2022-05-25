using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using TMPro;

namespace Game.Item
{
    public class ItensManager : Singleton<ItensManager>
    {
        public List<ItemSetup> itemSetups = new List<ItemSetup>();

        private void Start()
        {
            Reset();
            //RefreshUI();
        }

        private void Reset()
        {
            foreach (var item in itemSetups)
            {
                item.Value = 0;
            }
        }

        public static void AddItem(ItemType type, int amount = 1)
        {
            var item = Instance.itemSetups.Find(i => i.Type.Equals(type));

            if((item.Value + amount) < 0)
                item.Value = 0;
            else
                item.Value += amount;
        }

        private void RefreshUI()
        {
            foreach (var item in itemSetups)
            {
                item.UpdateUI();
            }
        }
    }
}