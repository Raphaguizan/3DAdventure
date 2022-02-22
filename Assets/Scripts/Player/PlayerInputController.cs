using UnityEngine;

namespace Game.Player
{
    public class PlayerInputController : MonoBehaviour
    {

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

            movement.Move(input);
        }

        private void GetKeyJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }
        }
        private void GetKeySprint()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.Run();
            }
        }
    }
}
