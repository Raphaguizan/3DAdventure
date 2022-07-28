using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Save
{
    public interface ISave 
    {
        public void Save();
        public void Load(SaveSetUp setup);
    }
}