using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Game.Sound;

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
        [SerializeField]
        private AudioClip _damageSound;
        [SerializeField]
        private AudioClip _healSound;
        [SerializeField]
        private AudioClip _dieMusic;

        [Space]
        public UnityEvent onHealEvent;
        public UnityEvent onDamageEvent;

        [SerializeField, Space]
        private bool _destoryObj = false;

        [SerializeField, ShowIf("_destoryObj")]
        private float _destoryTime = 1f;

        public UnityEvent onDieEvent;

        [HideInInspector]
        public bool loaded = false;

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
            _alive = true;
            if (loaded)
            {
                loaded = false;
                return;
            }
            ResetLife();
        }

        private void ResetLife()
        {
            _currentLife = _life;
        }

        public virtual void Damage(int damage, Vector3? direction = null)
        {
            if (!_damageable)
                return;

            _currentLife--;

            onDamageEvent.Invoke();
            if (_currentLife <= 0 && _alive)
            {
                Kill();
                return;
            }

            if (direction != null)
            {
                transform.DOMove((Vector3)direction, .1f);
            }

            foreach (var flash in _flashColor)
                flash.Flash();

            if (_damageSound) AudioPooling.Play(_damageSound, transform.position);
        }
        public virtual void SetHealth(int amount)
        {
            if (!_alive) return;
            if (amount <= 0)
            {
                Kill();
                return;
            }

            if (amount > _life) 
                _currentLife = _life;
            else
                _currentLife = amount;

            onHealEvent.Invoke();
        } 
        public virtual void AddHealth(int amount)
        {
            if (amount <= 0 || IsFull) return;

            _currentLife += amount;

            if(_currentLife > _life)
                _currentLife = _life;

            if (_healSound) AudioPooling.Play(_healSound, transform.position);

            onHealEvent.Invoke();
        }

        public virtual void Kill()
        {
            _alive = false;
            if (_dieMusic) MusicPooling.Play(_dieMusic);
            onDieEvent.Invoke();
            if (_destoryObj)
            {
                Destroy(gameObject, _destoryTime);
            }
        }

        public void MakeInvulnerable(float duration)
        {
            StartCoroutine (InvulnerableCoroutine(duration));
        }
        IEnumerator InvulnerableCoroutine(float duration)
        {
            _damageable = false;
            yield return new WaitForSeconds (duration);
            _damageable = true;
        }
    }
}

