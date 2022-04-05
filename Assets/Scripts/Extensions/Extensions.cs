using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public static class Extensions
    {
        public static Vector3 Randomize(this Vector3 v)
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        public static Vector2 Randomize(this Vector2 v)
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
