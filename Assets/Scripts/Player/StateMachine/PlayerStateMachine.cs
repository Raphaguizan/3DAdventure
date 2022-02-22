using Game.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.Player.StateMachine
{
    public class PlayerStateMachine : Singleton<PlayerStateMachine>
    {
        public StateMachineBase<PlayerStates> StateMachine => _stateMachine;
        private StateMachineBase<PlayerStates> _stateMachine;

        [SerializeField]
        private AnimationManager animation;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        public void Init()
        {
            _stateMachine = new StateMachineBase<PlayerStates>();
            _stateMachine.RegisterState(PlayerStates.IDLE, new PlayerStateIdle());
            _stateMachine.RegisterState(PlayerStates.WALK, new PlayerStateWalk());
            _stateMachine.RegisterState(PlayerStates.RUN, new PlayerStateRun());
            _stateMachine.RegisterState(PlayerStates.JUMP, new PlayerStateJump());

            _stateMachine.SwitchState(PlayerStates.IDLE);
        }

        public static StateBase CurrentState => Instance._stateMachine.CurrentState;
        public static bool CompareCurrentStateType(PlayerStates state) => state == Instance._stateMachine.CurrentStateType;
        public static void ChangeState(PlayerStates state, params object[] o) => Instance._stateMachine.SwitchState(state, Instance.animation, o);
    }
}

