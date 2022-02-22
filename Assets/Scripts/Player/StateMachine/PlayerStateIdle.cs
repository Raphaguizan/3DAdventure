using Game.Player;
using Game.StateMachine;
using Game.Util;

public class PlayerStateIdle : StateBase
{
    private AnimationManager anim;
    public override void OnStateEnter(params object[] o)
    {
        if (o.Find(typeof(AnimationManager)) is AnimationManager animation)
        {
            anim = animation;
            anim.Play(PlayerStates.WALK, false);
        }
    }
    public override void OnStateStay()
    {

    }
    public override void OnStateExit()
    {

    }
}
