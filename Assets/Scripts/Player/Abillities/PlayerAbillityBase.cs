using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Abillity
{
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerAbillityBase : MonoBehaviour
    {
        protected PlayerInputController player;

        private void OnValidate()
        {
            if(!player) player = GetComponent<PlayerInputController>();
        }
        private void Start()
        {
            Init(); 
            OnValidate();
            RegisterListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        protected void Init() { }

        protected void RegisterListeners() { }
        protected void RemoveListeners() { }
    }
}
