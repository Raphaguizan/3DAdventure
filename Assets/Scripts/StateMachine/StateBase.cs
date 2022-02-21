using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(object o = null)
        {
            Debug.Log("Entrou no estado");
        }
        public virtual void OnStateStay()
        {
            Debug.Log("Está no estado");
        }
        public virtual void OnStateExit()
        {
            Debug.Log("Saiu no estado");
        }
    }
}
