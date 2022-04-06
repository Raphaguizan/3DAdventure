using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachine
{
    public class StateMachineBase<T>: MonoBehaviour where T : System.Enum
    {
        #region declaration
        public StateBase CurrentState => _currentState;
        public T CurrentStateType => _currentStateType;
        public Dictionary<T, StateBase> Dictionary => dictionaryState;


        protected Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        private T _currentStateType;
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
            _currentStateType = state;
            _currentState = dictionaryState[state];
            _currentState.OnStateEnter(o);
        }

        private void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
        #endregion
    }
}
