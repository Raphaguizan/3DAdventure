using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;


namespace Game.Health
{
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
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Tween _currentTween;

        private void OnValidate()
        {
             _renderer = GetComponent<MeshRenderer>();
             _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        [Button]
        public void Flash()
        {
            if (!_currentTween.IsActive())
            {
                if (_renderer != null) 
                    _currentTween = _renderer.material.DOColor(_flashColor, "_EmissionColor", _flashDuration).SetEase(_ease).SetLoops(2, LoopType.Yoyo);
                if (_skinnedMeshRenderer != null)
                    _currentTween = _skinnedMeshRenderer.material.DOColor(_flashColor, "_EmissionColor", _flashDuration).SetEase(_ease).SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}
