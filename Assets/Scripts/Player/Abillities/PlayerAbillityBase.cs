using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Abillity
{
    public class PlayerAbillityBase : MonoBehaviour
    {
        private void Start()
        {
            Init(); 
            RegisterListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        protected virtual void Init() { }

        protected virtual void RegisterListeners() { }
        protected virtual void RemoveListeners() { }

        public virtual void StartAbillity(InputValue value) { }
        public virtual void EndAbillity() { }
    }
}
