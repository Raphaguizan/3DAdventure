using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

namespace Game.Enemy.Boss
{
    public enum BossActions
    {
        INIT,
        IDLE,
        WALK,
        ATTACK
    }
    public class BossBase : MonoBehaviour
    {
        private StateMachineBase<BossActions> _bossStateMachine = new StateMachineBase<BossActions>();

        public void Init()
        {
            _bossStateMachine.RegisterState(BossActions.WALK, new BossStateWalk());
        }
    }
}
