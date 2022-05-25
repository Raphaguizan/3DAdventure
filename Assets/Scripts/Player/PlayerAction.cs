using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Item;
using Game.Health;

namespace Game.Player
{
    public class PlayerAction : MonoBehaviour
    {
        public HealthBase health;
        public void MakeAction() 
        { 
            UseLifePack();
        }

        private void UseLifePack()
        {
            if (health.IsFull) return;
            if(ItensManager.AddItem(ItemType.LIFE_PACK, -1))
            {
                Debug.Log("cura True");
                health.AddHealth(5);
                return;
            }
            Debug.Log("cura false");
        }
    }
}