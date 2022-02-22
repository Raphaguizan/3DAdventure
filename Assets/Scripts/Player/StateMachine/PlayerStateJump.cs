using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;
using Game.Player;
using Game.Util;


public class PlayerStateJump : StateBase
{
    private AnimationManager anim;
    public override void OnStateEnter(params object[] o)
    {
        if (o.Find(typeof(AnimationManager)) is AnimationManager animM)
        {
            anim = animM;
            anim.Play(PlayerStates.JUMP, true);
        }
    }
    public override void OnStateStay()
    {

    }
    public override void OnStateExit()
    {
        anim.Play(PlayerStates.JUMP, false);
    }
}
