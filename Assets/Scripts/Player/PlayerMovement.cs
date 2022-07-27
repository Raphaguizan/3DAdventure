using DG.Tweening;
using Game.Player.StateMachine;
using System;
using System.Collections;
using UnityEngine;
using Game.CheckPoint;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool CanMove { get; set; }

        public CharacterController controller;
        public SO_DiePos diePos;

        public float speed = 10f;
        public float sprintModifier = 1.5f;
        public float rotateDuration = .5f;
        public float gravity = -9;
        public float jumpForce = 10;

        [Space, SerializeField]
        private ParticleSystem _walkParticle;
        [SerializeField]
        private ParticleSystem _landParticle;

        private Vector3 _moveDirection = Vector3.zero;
        private float _verticalSpeed = 0f;
        private float _initialSpeed;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            CanMove = true;
            _initialSpeed = speed;
        }

        IEnumerator Start()
        {
            yield return new WaitUntil(()=> CheckPointManager.LoadComplete);
            Vector3 resp = CheckPointManager.GetRespawnClosiestPos();
            if(resp != Vector3.zero)
                transform.position = resp;
            ChangeParticleWalk(true);
        }

        private void Update()
        {
            Gravity();
            controller.Move(_moveDirection);
            CheckLand();
        }

        private void Gravity()
        {
            _verticalSpeed += gravity * Time.deltaTime;
            _moveDirection.y = _verticalSpeed;
        }

        public void Run()
        {
            if (PlayerStateMachine.CompareCurrentStateType(PlayerStates.WALK))
            {
                PlayerStateMachine.ChangeState(PlayerStates.RUN);

                _moveDirection *= sprintModifier;
                _moveDirection.y = _verticalSpeed;
            }
        }

        public void Move(Vector2 direction)
        {
            if (!CanMove)
            {
                PlayerStateMachine.ChangeState(PlayerStates.IDLE);
                _moveDirection = Vector2.zero;
                return;
            }
            if (direction == Vector2.zero)
            {
                if (!PlayerStateMachine.CompareCurrentStateType(PlayerStates.JUMP))
                    PlayerStateMachine.ChangeState(PlayerStates.IDLE);

                _moveDirection = Vector2.zero;
            }
            else
            {
                if(!PlayerStateMachine.CompareCurrentStateType(PlayerStates.JUMP))
                    PlayerStateMachine.ChangeState(PlayerStates.WALK);
                // ajust by camera rotation
                Vector3 forwardProj = (Vector3.Project(Vector3.forward, Camera.main.transform.forward) + Camera.main.transform.forward).normalized * direction.y;
                Vector3 rightProj = (Vector3.Project(Vector3.right, Camera.main.transform.right) + Camera.main.transform.right).normalized * direction.x;

                _moveDirection = forwardProj + rightProj;
                AdjustRotation();
            }
            
            _moveDirection.y = _verticalSpeed;
            _moveDirection *= speed * Time.deltaTime;
        }
        private void AdjustRotation()
        {
            transform.DOLocalRotateQuaternion(Quaternion.LookRotation(new Vector3(_moveDirection.x, 0, _moveDirection.z)), rotateDuration);
        }

        public void Jump()
        {
            if (controller.isGrounded && !PlayerStateMachine.CompareCurrentStateType(PlayerStates.JUMP))
            {
                PlayerStateMachine.ChangeState(PlayerStates.JUMP);
                ChangeParticleWalk(false);
                _verticalSpeed = jumpForce;
            }
        }

        private void CheckLand()
        {
            if (controller.isGrounded && PlayerStateMachine.CompareCurrentStateType(PlayerStates.JUMP))
            {
                PlayerStateMachine.ChangeState(PlayerStates.IDLE);
                LandParticle();
                ChangeParticleWalk(true);
            }
        }

        public void SetDiePos()
        {
            ChangeParticleWalk(false);
            diePos.pos = transform.position;
        }

        public void SpeedBust(float mutiply, float duration)
        {
            StartCoroutine(SpeedBustCoroutine(mutiply, duration));
        }

        IEnumerator SpeedBustCoroutine(float mutiply, float duration)
        {
            speed *= mutiply;
            yield return new WaitForSeconds(duration);
            speed = _initialSpeed;
        }

        #region VFX
        public void ChangeParticleWalk(bool val)
        {
            if (!_walkParticle) return;
            if (val)
                _walkParticle.Play();
            else
                _walkParticle.Stop();
        }
        public void LandParticle()
        {
            if(_landParticle)
                _landParticle.Play();
        }
        #endregion
    }
}
