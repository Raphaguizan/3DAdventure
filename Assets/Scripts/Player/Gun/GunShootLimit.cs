using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.UI.Bullet;

namespace Game.Player.Gun
{
    public class GunShootLimit : GunBase
    {
        [Header("LimitGun")]
        [SerializeField]
        private GunUIRegister _gunUI;
        [SerializeField]
        private int _bulletLimit = 10;
        [SerializeField]
        private float _rechargeTime = 3f;

        private int _currentbullets = 0;

        protected override IEnumerator ShotController()
        {
            while (_buttonIsPressed)
            {
                if(_currentbullets < _bulletLimit && _canShoot)
                {
                    _currentbullets++;
                    Shot();
                    _gunUI.UpdateAll(_currentbullets, _bulletLimit);

                    if(_currentbullets >= _bulletLimit)
                    {
                        StartCoroutine(ReloadController());
                    }

                    yield return new WaitForSeconds(shotCooldown);
                }
                yield return null;
            }
        }

        protected virtual IEnumerator ReloadController()
        {
            _canShoot = false;
            float time = 0;
            while(time < _rechargeTime)
            {
                time += Time.deltaTime;
                _gunUI.UpdateAll(time/_rechargeTime);
                yield return null;
            }
            _currentbullets = 0;
            _canShoot = true;
        }
    }
}

