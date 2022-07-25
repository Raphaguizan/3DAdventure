using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    [SerializeField]
    private float _shakeDuration = .5f;
    [SerializeField]
    private int _shakeIntensity = 3;

    private Tween myTween;

    public void DamageActions()
    {
        if(myTween != null)myTween.Complete();
        myTween = transform.DOShakeScale(_shakeDuration, Vector3.up/2, _shakeIntensity);
    }

    public void DeathActions()
    {
        myTween.Complete();
        transform.DOScaleY(.1f, 2f);
    }
}
