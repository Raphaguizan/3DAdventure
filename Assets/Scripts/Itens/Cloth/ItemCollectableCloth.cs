using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cloth;

namespace Game.Item
{
    public class ItemCollectableCloth : ItemCollectableBase
    {
        [Header("Cloth"),SerializeField]
        protected ClothType _clothType = ClothType.DEFAULT;
        [SerializeField]
        protected float duration;

        protected override void Initialize()
        {
            type = ItemType.CLOTH;
        }

        protected override void OnCollet()
        {
            playerGO.GetComponent<ClothChanger>().ChangeCloth(_clothType, duration);
        }
    }
}