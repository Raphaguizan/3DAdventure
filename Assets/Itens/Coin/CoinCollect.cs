using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Item
{
    public class CoinCollect : ItemCollectableBase
    {
        public float animationSpeed = 1f;
        public float minDist = 1f;

        public ParticleSystem particles;
        protected override void Collect()
        {
            OnCollet();
            if (particles) particles.Play();
            DisableObj();
        }

        protected override void OnCollet()
        {
            //Update Interface
            ItensManager.AddItem(ItemType.COIN);
            Debug.Log("coletando moeda " + gameObject.name);
        }
    }
}