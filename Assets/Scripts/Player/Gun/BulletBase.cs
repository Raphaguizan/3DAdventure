using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Health;

public class BulletBase : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public int bulletDamage = 1;
    public TrailRenderer trail;

    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(gameObject.activeInHierarchy)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Initialize(Transform t, float speed)
    {
        this.speed = speed;
        transform.SetPositionAndRotation(t.position, t.rotation);

        gameObject.SetActive(true);
        if(trail)trail.Clear();
        StartCoroutine(TimeToDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(bulletDamage);
            gameObject.SetActive(false);
        }
    }
}
