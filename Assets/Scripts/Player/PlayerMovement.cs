using DG.Tweening;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool CanMove { get; set; }
        public bool IsMoving { get; private set; }
        public bool IsJumping { get; private set; }

        public CharacterController controller;
        public AnimationManager animationManager;
        public float speed = 10f;
        public float sprintModifier = 1.5f;
        public float rotateDuration = .5f;
        public float gravity = -9;
        public float jumpForce = 10;

        private Vector3 _moveDirection = Vector3.zero;
        private float _verticalSpeed = 0f;
        private void Start()
        {
            controller = GetComponent<CharacterController>();
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
            if (IsMoving)
            {
                _moveDirection *= sprintModifier;
                _moveDirection.y = _verticalSpeed;
                animationManager.Play(PlayerStates.RUN, true);
            }
        }

        public void Move(Vector2 direction)
        {
            // ajust by camera rotation
            Vector3 forwardProj = (Vector3.Project(Vector3.forward, Camera.main.transform.forward) + Camera.main.transform.forward).normalized * direction.y;
            Vector3 rightProj = (Vector3.Project(Vector3.right, Camera.main.transform.right) + Camera.main.transform.right).normalized * direction.x;

            _moveDirection = forwardProj + rightProj;
            _moveDirection.y = _verticalSpeed;
            _moveDirection *= speed * Time.deltaTime;

            if (direction != Vector2.zero)
            {
                AdjustRotation();
                IsMoving = true;
                animationManager.Play(PlayerStates.WALK, true);
            }
            else
            {
                IsMoving = false;
                animationManager.Play(PlayerStates.WALK, false);
            }
        }
        private void AdjustRotation()
        {
            transform.DOLocalRotateQuaternion(Quaternion.LookRotation(new Vector3(_moveDirection.x, 0, _moveDirection.z)), rotateDuration);
        }

        public void Jump()
        {
            if (controller.isGrounded && !IsJumping)
            {
                IsJumping = true;
                _verticalSpeed = jumpForce;
                animationManager.Play(PlayerStates.JUMP, true);
            }
        }

        private void CheckLand()
        {
            if (controller.isGrounded && IsJumping)
            {
                IsJumping = false;
                animationManager.Play(PlayerStates.JUMP, false);
            }
        }
    }
}
