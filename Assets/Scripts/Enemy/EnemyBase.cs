using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Animations;
using Game.Health;

namespace Game.Enemy
{

    public class EnemyBase : MonoBehaviour
    {
        [SerializeField]
        protected float _timeToDie;

        [Header("References")]
        [SerializeField]
        protected AnimationsController _animCtrl;
        [SerializeField]
        protected Collider _coll;

        protected bool isAlive = true;

        public virtual void Kill()
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
