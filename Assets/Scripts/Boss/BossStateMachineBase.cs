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
        ATTACK,
        DIE
    }
    public class BossStateMachineBase : StateMachineBase<BossActions>
    {
        protected override void InitialRegister()
        {
            RegisterState(BossActions.IDLE, new BossStateIdle());
            RegisterState(BossActions.WALK, new BossStateWalk());
            RegisterState(BossActions.INIT, new BossStateInit());
            RegisterState(BossActions.ATTACK, new BossStateAttack());
            RegisterState(BossActions.DIE, new BossStateDie());
        }

        public void Init(BossBase boss) => SwitchState(BossActions.INIT, boss);
  
        public void Idle(BossBase boss) => SwitchState(BossActions.IDLE, boss);

        public void Walk(BossBase boss) => SwitchState(BossActions.WALK, boss);
 
        public void Attack(BossBase boss, Transform player) => SwitchState(BossActions.ATTACK, boss, player);

        public void Die(BossBase boss) => SwitchState(BossActions.DIE, boss);

    }
}