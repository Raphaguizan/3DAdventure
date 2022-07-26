using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

namespace Game.Cloth
{
    public class ClothManager : Singleton<ClothManager>
    {
        [SerializeField]
        private List<ClothSetup> _clothList;
        public static ClothSetup GetClothByType(ClothType type)
        {
            return Instance._clothList.Find(x => x.type == type);
        }
    }
}

