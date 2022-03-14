using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public int Damage = 1;

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

    public void Initialize(Transform t)
    {
        transform.SetPositionAndRotation(t.position, t.rotation);

        gameObject.SetActive(true);
        StartCoroutine(TimeToDestroy());
    }
}
