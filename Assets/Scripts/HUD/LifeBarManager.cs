using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Health;
using DG.Tweening;

public class LifeBarManager : MonoBehaviour
{
    public HealthBase healthToTrack;
    public Image lifeBar;
    [Space, SerializeField]
    private float _animationDuration = .2f;
    [SerializeField]
    private Ease _animationEase = Ease.OutBack;

    private float maxLife;
    private Tween _tween;
    public void Start()
    {
        maxLife = healthToTrack.Life;
        healthToTrack.onDamageEvent.AddListener(UpdateInterface);
    }

    public void UpdateInterface()
    {
        if(_tween != null)
            if(_tween.IsPlaying())
                _tween.Complete();

        _tween = lifeBar.DOFillAmount((float)healthToTrack.CurrentLife / maxLife, _animationDuration).SetEase(_animationEase);
    }
}
