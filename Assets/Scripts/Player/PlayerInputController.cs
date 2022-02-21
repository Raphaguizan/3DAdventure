using UnityEngine;

namespace Game.Player
{
    public enum PlayerStates
    {
        IDLE,
        WALK,
        RUN,
        DIE,
        JUMP
    }
    public class PlayerInputController : MonoBehaviour
    {
        public AnimationManager animationManager;
        public PlayerMovement movement;

        private void Update()
        {
            GetKeyMovement();
            GetKeySprint();
            GetKeyJump();
        }

        private void GetKeyMovement()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector2 input = new Vector2(inputX, inputY);

            if (input != Vector2.zero)
            {
                animationManager.Play(PlayerStates.WALK, true);
            }
            else
            {
                animationManager.Play(PlayerStates.WALK, false);
            }

            movement.Move(input);
        }

        private void GetKeyJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (movement.controller.isGrounded)
                {
                    animationManager.Play(PlayerStates.JUMP);
                    movement.Jump();
                }
            }
        }
        private void GetKeySprint()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (movement.IsMoving)
                {
                    animationManager.Play(PlayerStates.RUN);
                    movement.Run();
                }
            }
        }
        private void Kill()
        {
            animationManager.Play(PlayerStates.DIE);
        }
    }

}
