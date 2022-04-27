using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;
using NaughtyAttributes;


namespace Game.Enemy.Boss
{
    
    public class BossBase : MonoBehaviour
    {
        public BossStateMachineBase stateMachine;

        private void Awake()
        {
            stateMachine.Initialize();
            stateMachine.Init(this);
        }

        [Button]
        public void Walk()
        {
            stateMachine?.Walk(this);
        }
    }
}
