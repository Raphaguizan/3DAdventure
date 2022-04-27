using Game.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CollisionDamage : MonoBehaviour
{
    [Tag]
    public string playerTag;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null && collision.gameObject.CompareTag(playerTag))
        {
            damageable.Damage(damage, (collision.transform.position - transform.position) *2);
        }
    }
}
