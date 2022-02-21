namespace Game.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(params object[] o)
        {
            //Debug.Log("Entrou no estado");
        }
        public virtual void OnStateStay()
        {
            //Debug.Log("Está no estado");
        }
        public virtual void OnStateExit()
        {
            //Debug.Log("Saiu no estado");
        }
    }
}

