using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;
using Game.Animations;

namespace Game.Enemy.Boss
{

    public class BossStateBase : StateBase
    {
        protected BossBase boss;
        public override void OnStateEnter(params object[] o)
        {
            base.OnStateEnter(o);
            boss = (BossBase)o[0];
        }
    }
    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] o)
        {
            base.OnStateEnter(o);
            boss.health.Damageable = false;
            boss.InitAnimation();
        }
        public override void OnStateExit()
        {
            boss.health.Damageable = true;
            boss.StopAllCoroutines();
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] o)
        {
            base.OnStateEnter(o);
            boss.animCtrl.Play(AnimationType.RUN);
            boss.NextPathPoint();
            boss.MoveToPoint();
        }
        public override void OnStateExit()
        {
            boss.StopAllCoroutines();
        }
    }
   
    public class BossStateIdle : BossStateBase
    {
        public override void OnStateEnter(params object[] o)
        {
            base.OnStateEnter(o);
            boss.Idle();
            boss.animCtrl.Play(AnimationType.IDLE);
        }
        public override void OnStateExit()
        {
            boss.StopAllCoroutines();
        }
    }
    public class BossStateAttack : BossStateBase
    {
        Transform player;
        public override void OnStateEnter(params object[] o)
        {
            base.OnStateEnter(o);
            player = (Transform)o[1];

            boss.InitAttack();
            boss.animCtrl.Play(AnimationType.ATTACK);
        }
        public override void OnStateStay()
        {
            boss.LookAtTarget(player);
        }
        public override void OnStateExit()
        {
            boss.gun.EndShoot();
            boss.StopAllCoroutines();
        }
    }
    public class BossStateDie : BossStateBase
    {
        public override void OnStateEnter(params object[] o)
        {
            base.OnStateEnter(o);
            boss.health.Damageable = false;
            boss.animCtrl.Play(AnimationType.DIE);
        }
    }
}