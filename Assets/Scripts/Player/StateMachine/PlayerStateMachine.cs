using Game.Player.StateMachine;
using Game.StateMachine;
using UnityEngine;

namespace Game.Player.StateMachine
{
    public enum PlayerStates
    {
        IDLE,
        WALK,
        DIE,
        JUMP
    }
    public class PlayerStateMachine : MonoBehaviour
    {
        public AnimationManager animationManager;
        public StateMachineBase<PlayerStates> StateMachine => _stateMachine;
        private StateMachineBase<PlayerStates> _stateMachine;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _stateMachine = new StateMachineBase<PlayerStates>();
            _stateMachine.RegisterState(PlayerStates.IDLE, new PlayerStateIdle());
            _stateMachine.RegisterState(PlayerStates.WALK, new PlayerStateWalk());
            _stateMachine.RegisterState(PlayerStates.JUMP, new PlayerStateJump());
            _stateMachine.RegisterState(PlayerStates.DIE, new PlayerStateDie());
        }

        private void Update()
        {
            GetKeyMovement();
            GetKeyJump();
        }

        private void GetKeyMovement()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector2 input = new Vector2(inputX, inputY);

            if (input != Vector2.zero)
                _stateMachine.SwitchState(PlayerStates.WALK, input, animationManager);
            else
                _stateMachine.SwitchState(PlayerStates.IDLE, null, animationManager);
        }

        private void GetKeyJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stateMachine.SwitchState(PlayerStates.JUMP, null, animationManager);
            }
        }
        private void Kill()
        {
            _stateMachine.SwitchState(PlayerStates.DIE, null, animationManager);
        }
    }

}
