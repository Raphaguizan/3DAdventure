using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.CheckPoint
{
    public class CheckPointBase : MonoBehaviour
    {
        public int keyValue;
        [SerializeField, Tag]
        private string PlayerTag = "Player";
        [SerializeField, ColorUsage(true, true)]
        private Color _activeColor;

        private MeshRenderer render;
        private bool _active = false;

        public bool Active => _active;
        private void Awake()
        {
            render = GetComponentInChildren<MeshRenderer>();
        }

        private void Start()
        {
            CheckPointManager.RegisterCheckPoint(this);
            if (CheckPointManager.CheckIsUnloked(keyValue))
            {
                TurnOn();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(PlayerTag))
            {
                TurnOn();
            }
        }

        private void TurnOn()
        {
            if (_active)
                return;

            CheckPointManager.SaveCheckPoint(this);
            if(render) render.material.SetColor("_EmissionColor",_activeColor);

            _active = true;
        }
        private void OnDestroy()
        {
            CheckPointManager.UnregisterCheckPoint(this);
        }
    }
}