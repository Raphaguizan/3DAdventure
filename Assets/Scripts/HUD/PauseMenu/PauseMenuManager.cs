using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Save;

public class PauseMenuManager : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void MenuToggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SaveButton()
    {
        SaveManager.Save();
    }

    public void LoadButton()
    {
        SaveManager.Load();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
