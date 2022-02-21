using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;
using Game.Util;

namespace Game.Player.StateMachine
{
    public class PlayerStateIdle : StateBase
    {
        AnimationManager anim;
        public override void OnStateEnter(params object[] o)
        {
            if (anim == null)
                anim = o.Find(typeof(AnimationManager)) as AnimationManager;

            anim.Play(PlayerStates.WALK, false);
        }
        public override void OnStateStay()
        {

        }
        public override void OnStateExit()
        {

        }
    }

    public class PlayerStateWalk : StateBase
    {
        AnimationManager anim;
        Vector2 direction;
        public override void OnStateEnter(params object[] o)
        {
            if (anim == null)
                anim = o.Find(typeof(AnimationManager)) as AnimationManager;
            anim.Play(PlayerStates.WALK, true);
        }

        public override void OnStateStay()
        {

        }
        public override void OnStateExit()
        {

        }
    }

    public class PlayerStateJump : StateBase
    {
        AnimationManager anim;
        public override void OnStateEnter(params object[] o)
        {
            if (anim == null)
                anim = o.Find(typeof(AnimationManager)) as AnimationManager;
            anim.Play(PlayerStates.JUMP);
        }

        public override void OnStateStay()
        {

        }
        public override void OnStateExit()
        {

        }
    }
    public class PlayerStateDie : StateBase
    {
        AnimationManager anim;
        public override void OnStateEnter(params object[] o)
        {
            if (anim == null)
                anim = o.Find(typeof(AnimationManager)) as AnimationManager;
            anim.Play(PlayerStates.DIE);
        }

        public override void OnStateStay()
        {

        }
        public override void OnStateExit()
        {

        }
    }
}
