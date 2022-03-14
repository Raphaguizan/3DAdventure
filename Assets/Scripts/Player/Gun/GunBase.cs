using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform gunPoint;
    public float shotCooldown;
    public GameObject bulletPrefab;
    [Space]
    public KeyCode shotKey = KeyCode.A;
    public Action ShotCallBack;

    private List<GameObject> _shotPoolingList = new List<GameObject>();
    private Coroutine _currentCoroutine;
    private bool _buttonIsPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(shotKey))
        {
            _buttonIsPressed = true;
            _currentCoroutine = StartCoroutine(ShotController());
        }
        else if (Input.GetKeyUp(shotKey))
        {
            _buttonIsPressed = false;
            StopCoroutine(_currentCoroutine);
        }
    }
    IEnumerator ShotController()
    {
        while (_buttonIsPressed)
        {
            Shot();
            yield return new WaitForSeconds(shotCooldown);
        }
    }

    private void Shot()
    {
        ShotCallBack?.Invoke();

        foreach(var i in _shotPoolingList)
        {
            if (!i.activeInHierarchy)
            {
                i.GetComponent<BulletBase>().Initialize(gunPoint.transform);
                return;
            }
        }
        var aux = Instantiate(bulletPrefab);
        aux.GetComponent<BulletBase>().Initialize(gunPoint.transform);
        _shotPoolingList.Add(aux);
    }
}
