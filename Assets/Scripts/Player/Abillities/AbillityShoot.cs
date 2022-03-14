using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Player.Gun;

namespace Game.Player.Abillity
{
    public class AbillityShoot : PlayerAbillityBase
    {
       public GunBase gunBase;
        public override void StartAbillity(InputValue value)
        {
            gunBase.StartShoot();
        }
        public override void EndAbillity()
        {
            gunBase.EndShoot();
        }
    }
}
