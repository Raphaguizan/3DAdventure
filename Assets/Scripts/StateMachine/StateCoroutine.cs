using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.StateMachine
{
    public class StateCoroutine : Singleton<StateCoroutine>
    {
        public static void StartStateCoroutine(IEnumerator enumerator)
        {
            Instance.StartCoroutine(enumerator);
        }
    }
}
