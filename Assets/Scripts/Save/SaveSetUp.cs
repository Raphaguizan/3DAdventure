using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Save
{
    [Serializable]
    public class SaveSetUp
    {
        public int level;
        public int life;
        public int coins;
        public int lifePack;
        public List<int> checkPoints;
        public Vector3 lastPosition;

        public SaveSetUp(int level = 1, int life = 10, int coins = 0, int lifePack = 0, List<int> checkPoints = null, Vector3? lastPosition = null)
        {
            this.level = level;
            this.life = life;
            this.coins = coins;
            this.lifePack = lifePack;
            this.checkPoints = checkPoints;

            if (lastPosition == null)
                lastPosition = new Vector3(0.850006104f, -1.79999971f, -11.7075462f);

            this.lastPosition = (Vector3)lastPosition;
        }
    }
}