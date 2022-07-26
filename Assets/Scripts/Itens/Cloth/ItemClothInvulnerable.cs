using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cloth;
using Game.Health;

namespace Game.Item
{
    public class ItemClothInvulnerable : ItemCollectableCloth
    {
        protected override void Initialize()
        {
            base.Initialize();
            _clothType = ClothType.INVULNERABLE;
        }
        protected override void OnCollet()
        {
            base.OnCollet();
            playerGO.GetComponent<HealthBase>().MakeInvulnerable(duration);
        }
    }
}
