using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;


namespace Game.Enemy
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FlashColor : MonoBehaviour
    {
        [Header("Anim Setup")]
        [SerializeField]
        private float _flashDuration = .1f;
        [SerializeField]
        private Ease _ease = Ease.OutBack;
        [SerializeField]
        private Color _flashColor = Color.white;

        private MeshRenderer _renderer;
        private Tween _currentTween;

        private void OnValidate()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        [Button]
        public void Flash()
        {
            if (!_currentTween.IsActive())
                _currentTween = _renderer.material.DOColor(_flashColor, "_EmissionColor", _flashDuration).SetEase(_ease).SetLoops(2, LoopType.Yoyo);
        }
    }
}
