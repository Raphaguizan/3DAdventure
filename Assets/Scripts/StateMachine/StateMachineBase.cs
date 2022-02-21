using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachine
{
    public class StateMachineBase<T> where T : System.Enum
    {
        #region declaration
        public StateBase CurrentState => _currentState;
        public Dictionary<T, StateBase> Dictionary => dictionaryState;


        protected Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        #endregion

        #region init
        public StateMachineBase()
        {
            dictionaryState = new Dictionary<T, StateBase>();
            StateBase statebase = new StateBase();
            string[] enumString = System.Enum.GetNames(typeof(T));

            for (int i = 0; i < enumString.Length; i++)
            {
                RegisterState((T)System.Enum.Parse(typeof(T), enumString[i]), statebase);
            }
            StateCoroutine.StartStateCoroutine(StateUpdate());
        }

        public void RegisterState(T stateType, StateBase stateClass)
        {
            if (dictionaryState.ContainsKey(stateType))
                dictionaryState[stateType] = stateClass;
            else
                dictionaryState.Add(stateType, stateClass);
        }
        #endregion

        #region states controller
        public void SwitchState(T state, params object[] o)
        {
            if (_currentState != null) _currentState.OnStateExit();
            _currentState = dictionaryState[state];
            _currentState.OnStateEnter(o);
        }

        IEnumerator StateUpdate()
        {
            while (true)
            {
                if (_currentState != null) _currentState.OnStateStay();
                yield return null;
            }
        }
        #endregion
    }
}
