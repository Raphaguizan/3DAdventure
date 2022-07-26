using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.Health;

public class DestructableItemBase : MonoBehaviour
{
    [SerializeField]
    private HealthBase health;

    [Header("Shake feedback"), SerializeField]
    private float _shakeDuration = .5f;
    [SerializeField]
    private int _shakeIntensity = 3;

    [Header("Item spawn"), SerializeField]
    private GameObject _item;
    [SerializeField]
    private Transform _spawnTransform;
    [SerializeField]
    private float _spawnForce = 5f;
    [SerializeField]
    private int _maxItem = 10;

    [Space, Range(0, 1), SerializeField]
    private float _SpawnChance = .5f;

    private Tween myTween;

    public void DamageActions()
    {
        if(myTween != null)myTween.Complete();
        myTween = transform.DOShakeScale(_shakeDuration, Vector3.up/2, _shakeIntensity);

        if(_maxItem <= 0)
        {
            health.Kill();
            return;
        }

        if(Random.value <= _SpawnChance)
            SpawnItem();
    }

    public void DeathActions()
    {
        myTween.Complete();
        transform.DOScaleY(.1f, 2f);
    }

    public void SpawnItem()
    {
        var newItem = Instantiate(_item);
        _maxItem--;
        newItem.transform.position = _spawnTransform.position;
        newItem.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere.normalized * _spawnForce, ForceMode.Impulse);
    }
}
