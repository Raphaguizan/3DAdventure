using DG.Tweening;
using Game.Animations;
using Game.Health;
using Game.Player.Gun;
using System.Collections;
using UnityEngine;


namespace Game.Enemy.Boss
{

    public class BossBase : MonoBehaviour
    {
        public BossStateMachineBase stateMachine;
        public AnimationsController animCtrl;
        public HealthBase health;
        public GunBase gun;

        [Space]
        public Transform path;
        public Transform player;

        [Space]
        public float speed = 10f;
        public float minPointDist = .1f;
        public float rotationDuration = .1f;
        [Space]
        public Vector2 randomIdleTime;
        public Vector2 randomAttackTime;

        private int _pathIndex = 0;
        private Transform _currentPathPoint;
        private Tween _currentRotationTween;
        private Tween _currentInitTween;
        private Coroutine _moveCoroutine;

        private void OnEnable()
        {
            stateMachine.Initialize();
            stateMachine.Init(this);
        }

        #region ACTIONS CONTROLLER
        private void NextAction()
        {
            int nexAction = Random.Range(0, 3);
            switch (nexAction)
            {
                case 0:
                    stateMachine.Walk(this);
                    break;
                case 1:
                    stateMachine.Idle(this);
                    break;
                case 2:
                    stateMachine.Attack(this, player);
                    break;
            }
        }

        IEnumerator WaitTimeToChangeAction(float sec)
        {
            yield return new WaitForSeconds(sec);
            NextAction();
        }
        #endregion

        #region INIT ANIMATION
        public void InitAnimation()
        {
            _currentInitTween = transform.DOScale(0, 5f).SetEase(Ease.OutElastic).From();
            StartCoroutine(WaitInitAnimation());
        }
        IEnumerator WaitInitAnimation()
        {
            yield return new WaitWhile(() => _currentInitTween.IsPlaying());
            NextAction();
        }
        #endregion

        #region MOVEMENT
        public void NextPathPoint()
        {
            _currentPathPoint = path.GetChild(_pathIndex);
            _pathIndex++;
            if (_pathIndex >= path.childCount)
                _pathIndex = 0;
        }

        public void MoveToPoint()
        {
            LookAtTarget(_currentPathPoint);
            _moveCoroutine = StartCoroutine(MoveCouroutine());
        }

        IEnumerator MoveCouroutine()
        {
            yield return new WaitWhile(() => _currentRotationTween.IsPlaying());
            while (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_currentPathPoint.position.x, _currentPathPoint.position.z)) > minPointDist)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                AdjustYHeigth();
                yield return null;
            }
            NextAction();
        }
        public void LookAtTarget(Transform target)
        {
            _currentRotationTween = transform.DOLookAt(target.position, rotationDuration, AxisConstraint.None, Vector3.up);
        }
        private void AdjustYHeigth()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    Vector3 aux = transform.position;
                    aux.y = hit.transform.position.y;
                    transform.position = aux;
                }
            }
        }
        #endregion

        #region ATTACK
        public void InitAttack()
        {
            gun.StartShoot();
            StartCoroutine(WaitTimeToChangeAction(Random.Range(randomAttackTime.x, randomAttackTime.y)));
        }
        #endregion

        #region IDLE
        public void Idle()
        {
            StartCoroutine(WaitTimeToChangeAction(Random.Range(randomIdleTime.x, randomIdleTime.y)));
        }
        #endregion

        #region DIE
        public void Kill()
        {
            stateMachine.Die(this);
            Destroy(gameObject, 5f);
        }
        #endregion
    }
}
