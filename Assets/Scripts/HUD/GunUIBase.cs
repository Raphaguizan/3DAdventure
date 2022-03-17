using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Bullet
{
    public class GunUIBase : MonoBehaviour
    {
        [SerializeField]
        private GunUIRegister register;
        private void Awake()
        {
            register.Register(this);
        }

        public virtual void UpdateUI(float val) { }
        public virtual void UpdateUI(float val, int max) { }
    }
}

