using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using Game.Save;

namespace Game.Item
{
    public class ItensManager : Singleton<ItensManager>, ISave
    {
        public List<ItemSetup> itemSetups = new List<ItemSetup>();

        private bool loaded = false;

        protected override void Awake()
        {
            base.Awake();

            Load(SaveManager.setUp);

            SaveManager.Loaded += Load;
            SaveManager.ToSave += Save;
        }

        private void Start()
        {
            if (loaded)
            {
                loaded = false;
                RefreshUI();
                return;
            }
            Reset();
        }

        public void Reset()
        {
            foreach (var item in itemSetups)
            {
                item.Value = 0;
            }
        }

        public static bool AddItem(ItemType type, int amount = 1)
        {
            var item = FindItemByType(type);

            if((item.Value + amount) < 0)
            {
                item.Value = 0;
                return false;
            }
            else
            {
                item.Value += amount;
                return true;
            }
        }

        public static ItemSetup FindItemByType(ItemType type)
        {
            return Instance.itemSetups.Find(x => x.Type == type);
        }

        private void RefreshUI()
        {
            foreach (var item in itemSetups)
            {
                item.UpdateUI();
            }
        }

        public void Save()
        {
            SaveManager.setUp.coins = FindItemByType(ItemType.COIN).Value;
            SaveManager.setUp.lifePack = FindItemByType(ItemType.LIFE_PACK).Value;
        }

        public void Load(SaveSetUp setup)
        {
            loaded = true;
            FindItemByType(ItemType.COIN).Value = setup.coins;
            FindItemByType(ItemType.LIFE_PACK).Value = setup.lifePack;
        }

        private void OnDestroy()
        {
            SaveManager.Loaded -= Load;
            SaveManager.ToSave -= Save;
        }
    }
}