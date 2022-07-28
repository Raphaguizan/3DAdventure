using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.Player.Abillity;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputController : MonoBehaviour
    {

        public PlayerMovement movement;
        public PlayerAbillityBase abillity;
        public PlayerAction actions;

        [Space]
        public UnityEvent OnPauseCallBack;

        public static Action OnInteractCallBack;

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

        private void OnShoot(InputValue value)
        {
            if (value.isPressed)
                abillity.StartAbillity(value);
            else
                abillity.EndAbillity();
        }
        private void OnChangeGun(InputValue value)
        {
            if(abillity is AbillityShoot)
            {
                AbillityShoot aux = (AbillityShoot)abillity;
                aux.ChangeGun((int)value.Get<float>() -1);
            }
        }

        private void OnUseItem(InputValue value)
        {
            actions.MakeAction();
        }

        private void OnInteract(InputValue value)
        {
            OnInteractCallBack?.Invoke();
        }

        private void OnPause()
        {
            OnPauseCallBack.Invoke();
        }
    }
}
