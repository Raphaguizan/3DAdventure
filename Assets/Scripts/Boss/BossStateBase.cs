using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

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

    public class BossStateWalk : BossStateBase
    {

    }
}