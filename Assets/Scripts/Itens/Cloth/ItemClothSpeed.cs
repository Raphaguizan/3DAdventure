using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cloth;
using Game.Player;

namespace Game.Item
{
    public class ItemClothSpeed : ItemCollectableCloth
    {
        [SerializeField]
        private float _speedMultiplier;

        protected override void Initialize()
        {
            base.Initialize();
            _clothType = ClothType.SPEED;
        }
        protected override void OnCollet()
        {
            base.OnCollet();
            playerGO.GetComponent<PlayerMovement>().SpeedBust(_speedMultiplier, duration);
        }
    }
}