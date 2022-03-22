using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Player.Gun;

namespace Game.Player.Abillity
{
    public class AbillityShoot : PlayerAbillityBase
    {
        public GunChoiceUIController gunUI;
        public List<GunBase> guns;


        private GunBase _currentGun = null;
        private List<GunBase> _instantiatedGuns = new List<GunBase>();
        private void Start()
        {
            gunUI.Initialize(guns.Count);
            InitializeGuns();
        }

        private void InitializeGuns()
        {
            foreach (GunBase gun in guns)
            {
                GunBase gunAux =  Instantiate(gun, transform);
                _instantiatedGuns.Add(gunAux);
            }
            ChangeGun(0);
        }

        public void ChangeGun(int num)
        {
            if (num >= guns.Count) return;
            if (_currentGun)
            {
                _currentGun.DisableGun();
            } 

            _currentGun = _instantiatedGuns[num];
            _currentGun.InitGun();
            gunUI.ChangeUIGun(num);
        }

        public void AddGun(GunBase newGun)
        {
            guns.Add(newGun);
            gunUI.AddGun();
        }

        public override void StartAbillity(InputValue value)
        {
            _currentGun.StartShoot();
        }
        public override void EndAbillity()
        {
            _currentGun.EndShoot();
        }
    }
}
