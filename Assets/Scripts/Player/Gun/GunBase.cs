using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Gun
{
    public class GunBase : MonoBehaviour
    {
        public Transform gunPoint;
        public GameObject bulletPrefab;
        public float shotCooldown;
        public Action ShotCallBack;

        private List<GameObject> _shotPoolingList = new List<GameObject>();
        private Coroutine _currentCoroutine;

        protected bool _canShoot = true;
        protected bool _buttonIsPressed = false;

        protected virtual IEnumerator ShotController()
        {
            while (_buttonIsPressed)
            {
                Shot();
                yield return new WaitForSeconds(shotCooldown);
            }
        }

        protected virtual void Shot()
        {
            if (!_canShoot) return;
            ShotCallBack?.Invoke();

            foreach (var i in _shotPoolingList)
            {
                if (!i.activeInHierarchy)
                {
                    i.GetComponent<BulletBase>().Initialize(gunPoint.transform);
                    return;
                }
            }
            var aux = Instantiate(bulletPrefab);
            aux.GetComponent<BulletBase>().Initialize(gunPoint.transform);
            _shotPoolingList.Add(aux);
        }

        public virtual void StartShoot()
        {
            _buttonIsPressed = true;
            _currentCoroutine = StartCoroutine(ShotController());
        }
        public virtual void EndShoot()
        {
            _buttonIsPressed = false;
            if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }
    }
}
