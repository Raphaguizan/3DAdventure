using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChoiceUIController : MonoBehaviour
{
    public GameObject prefab;
    public Transform container;

    private GunChoiceUI _currentGun = null;
    private List<GunChoiceUI> _choices = new List<GunChoiceUI>();

    public void Initialize(int num)
    {
        for (int i = 0; i < num; i++)
        {
            AddGun();
        }
    }

    public void ChangeUIGun(int num)
    {
        if(_currentGun)_currentGun.ResetColor();
        _currentGun = _choices[num];
        _currentGun.Select();
    }

    public void AddGun()
    {
        GameObject aux = Instantiate(prefab, container);

        GunChoiceUI choiceAux = aux.GetComponent<GunChoiceUI>();
        choiceAux.Init(_choices.Count);
        _choices.Add(choiceAux);
    }
}
