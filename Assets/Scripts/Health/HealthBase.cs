using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Game.Health
{
    public class HealthBase : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private bool _damageable = true;
        [SerializeField]
        private int _life = 10;
        [SerializeField, ReadOnly]
        private int _currentLife = 0;

        [Space]
        public UnityEvent onHealEvent;
        public UnityEvent onDamageEvent;

        [SerializeField, Space]
        private bool _destoryObj = false;

        [SerializeField, ShowIf("_destoryObj")]
        private float _destoryTime = 1f;

        public UnityEvent onDieEvent;


        public int Life => _life;
        public int CurrentLife => _currentLife;
        public bool IsFull => CurrentLife == Life;
        public bool Damageable { get => _damageable; set => _damageable = value; }

        private FlashColor[] _flashColor;
        private bool _alive = true;

        #region Teste
        [Button]
        public void TestDamage()
        {
            Damage(2);
        }
        #endregion
        private void OnValidate()
        {
            if(_flashColor == null)_flashColor = GetComponentsInChildren<FlashColor>();
        }

        private void OnEnable()
        {
            _currentLife = _life;
            _alive = true;
        }

        public virtual void Damage(int damage, Vector3? direction = null)
        {
            if (!_damageable)
                return;

            _currentLife--;

            if (_currentLife <= 0 && _alive)
            {
                Kill();
                return;
            }

            if (direction != null)
            {
                transform.DOMove((Vector3)direction, .1f);
            }
            onDamageEvent.Invoke();

            foreach (var flash in _flashColor)
                flash.Flash();
        }

        public virtual void AddHealth(int amount)
        {
            if (amount <= 0 || IsFull) return;

            _currentLife += amount;
            onHealEvent.Invoke();

            if(_currentLife > _life)
                _currentLife = _life;
        }

        public virtual void Kill()
        {
            _alive = false;
            onDieEvent.Invoke();
            if (_destoryObj)
            {
                Destroy(gameObject, _destoryTime);
            }
        }

        public void MakeIvunerable(float duration)
        {
            StartCoroutine (IvunerableCoroutine(duration));
        }
        IEnumerator IvunerableCoroutine(float duration)
        {
            _damageable = false;
            yield return new WaitForSeconds (duration);
            _damageable = true;
        }
    }
}

