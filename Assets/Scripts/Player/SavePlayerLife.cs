using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Health;
using Game.Save;

namespace Game.Player {
    public class SavePlayerLife : MonoBehaviour, ISave
    {
        [SerializeField]
        private HealthBase _health;

        private void Awake()
        {
            if(SaveManager.LoadRequired)
                Load(SaveManager.setUp);

            SaveManager.Loaded += Load;
            SaveManager.ToSave += Save;
        }

        public void Load(SaveSetUp setup)
        {
            if (setup.life == 0) return;

            _health.loaded = true;
            _health.SetHealth(setup.life);
        }

        public void Save()
        {
            SaveManager.setUp.life = _health.CurrentLife;
        }

        private void OnDestroy()
        {
            SaveManager.Loaded -= Load;
            SaveManager.ToSave -= Save;
        }
    }
}