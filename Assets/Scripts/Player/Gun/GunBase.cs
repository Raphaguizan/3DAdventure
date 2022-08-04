using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Sound;

namespace Game.Player.Gun
{
    public class GunBase : MonoBehaviour
    {
        [Header("base gun")]
        public Transform gunPoint;
        public GameObject bulletPrefab;
        public float shotCooldown = .2f;
        public float bulletSpeed = 25f;
        public Action ShotCallBack;
        public AudioClip shotSound;

        private List<GameObject> _shotPoolingList = new List<GameObject>();
        private Coroutine _currentCoroutine;

        protected bool _canShoot = true;
        protected bool _buttonIsPressed = false;
        protected bool _mainGun = false;

        private void OnEnable()
        {
            InitGun();
        }
        public virtual void InitGun()
        {
            _mainGun = true;
        }
        public virtual void DisableGun()
        {
            _mainGun = false;
            EndShoot();
        }

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

            BulletPooling(gunPoint);
            if (shotSound) AudioPooling.Play(shotSound, transform.position);
        }

        protected void BulletPooling(Transform bulletParent)
        {
            foreach (var i in _shotPoolingList)
            {
                if (!i.activeInHierarchy)
                {
                    InitializeBullet(i.GetComponent<BulletBase>(), bulletParent);
                    return;
                }
            }
            var aux = Instantiate(bulletPrefab);
            InitializeBullet(aux.GetComponent<BulletBase>(), bulletParent);
            _shotPoolingList.Add(aux);
        }

        protected virtual void InitializeBullet(BulletBase bullet, Transform t)
        {
            bullet.Initialize(t, bulletSpeed);
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
