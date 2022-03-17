using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.Bullet;
using System;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Game/UI/Gun Register", fileName = "Gun Register")]
    public class GunUIRegister : ScriptableObject
    {
        private List<GunUIBase> gunUIList;

        public void Register(GunUIBase gunUI)
        {
            gunUIList.Add(gunUI);
        }
        public void Unregister(GunUIBase gunUI)
        {
            gunUIList.Remove(gunUI);
        }

        public void UpdateAll(float val)
        {
            foreach (GunUIBase gunUI in gunUIList)
                gunUI.UpdateUI(val);
        }
        public void UpdateAll(float val, int max)
        {
            foreach (GunUIBase gunUI in gunUIList)
                gunUI.UpdateUI(val, max);
        }
    }
}
