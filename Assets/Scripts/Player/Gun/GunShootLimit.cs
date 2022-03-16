using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Gun
{
    public class GunShootLimit : GunBase
    {
        [Header("LimitGun")]
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
                    yield return new WaitForSeconds(shotCooldown);
                }
                else if (_canShoot)
                {
                    StartCoroutine(ReloadController());
                }
                yield return null;
            }
        }

        protected virtual IEnumerator ReloadController()
        {
            _canShoot = false;
            _currentbullets = 0;
            yield return new WaitForSeconds(_rechargeTime);
            _canShoot = true;
        }
    }
}

