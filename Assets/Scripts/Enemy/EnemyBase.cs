using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Animations;

namespace Game.Enemy
{

    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField]
        protected int life = 5;

        [SerializeField]
        protected float _timeToDie;

        [Header("References")]
        [SerializeField]
        protected AnimationsController _animCtrl;
        [SerializeField]
        protected Collider _coll;
        [SerializeField]
        protected FlashColor _flashColor;

        protected bool isAlive = true;

        public virtual void Damage(int damage)
        {
            life -= damage;
            _flashColor.Flash();
            if (life <= 0)
                Kill();
        }

        protected virtual void Kill()
        {
            if(_coll != null)_coll.enabled = false;
            PlayAnimation(AnimationType.DIE);
            OnKill();
        }

        protected virtual void OnKill()
        {
            isAlive = false;
            Destroy(gameObject, _timeToDie);
        }

        protected void PlayAnimation(AnimationType type)
        {
            _animCtrl.Play(type);
        }
    }
}
