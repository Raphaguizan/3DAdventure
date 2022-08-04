using Game.Player.Gun;
using UnityEngine;
using Game.Sound;

public class GunMutipleShot : GunShootLimit
{
    [Header("Multiple Shot")]
    [SerializeField]
    private int _bulletAmount = 3;
    [SerializeField]
    private float _angle = 13f;

    private Vector3 gunPointDefaultRot;
    private void Start()
    {
        if(_bulletAmount%2 == 0)
            gunPointDefaultRot = gunPoint.localEulerAngles = Vector3.zero + Vector3.up * _angle/2;
        else
            gunPointDefaultRot = gunPoint.localEulerAngles = Vector3.zero;
    }
    private void OnValidate()
    {
        Start();
    }
    protected override void Shot()
    {
        if (!_canShoot) return;
        ShotCallBack?.Invoke();

        ResetGunPoint();
        int mult = 0;
        for (int i = 0; i < _bulletAmount; i++)
        {
            if (i % 2 != 0) mult++;
            gunPoint.localEulerAngles = gunPointDefaultRot + Vector3.up * (i % 2 == 0 ? _angle : -_angle) * mult;
            BulletPooling(gunPoint);
        }
        ResetGunPoint();
        if (shotSound) AudioPooling.Play(shotSound, transform.position);
    }

    private void ResetGunPoint()
    {
        gunPoint.localEulerAngles = gunPointDefaultRot;
    }
}
