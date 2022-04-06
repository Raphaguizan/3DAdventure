using UnityEngine;

namespace Game.Health
{
    public interface IDamageable
    {
        public void Damage(int damage, Vector3? direction = null);
    }
}