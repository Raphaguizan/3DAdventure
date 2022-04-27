using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Health
{
    public class HealthBase : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _life = 10;

        [SerializeField, ReadOnly]
        private int _currentLife = 0;

        [SerializeField, Space]
        private bool _destoryObj = false;

        [SerializeField, ShowIf("_destoryObj")]
        private float _destoryTime = 1f;

        public UnityEvent onDieEvent;


        public int Life => _life;
        public int CurrentLife => _currentLife;


        private FlashColor[] _flashColor;

        #region Teste
        [Button]
        public void TestDamage()
        {
            Damage(2);
        }
        #endregion
        private void OnValidate()
        {
            _flashColor = GetComponentsInChildren<FlashColor>();
        }

        private void Start()
        {
            _currentLife = _life;
        }

        public virtual void Damage(int damage, Vector3? direction = null)
        {
            if (direction != null)
            {
                transform.DOMove((Vector3)direction - transform.position, .1f);
            }
            _currentLife--;

            foreach (var flash in _flashColor)
                flash.Flash();

            if (_currentLife <= 0) Kill();
        }

        public virtual void Kill()
        {
            if (_destoryObj)
            {
                Destroy(gameObject, _destoryTime);
            }
            else
            {
                onDieEvent.Invoke();
            }
        }
    }
}

