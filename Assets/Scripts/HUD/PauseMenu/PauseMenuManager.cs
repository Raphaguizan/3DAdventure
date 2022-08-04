using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.Save;

public class PauseMenuManager : MonoBehaviour
{
    public UnityEvent OnPauseOn;
    public UnityEvent OnPauseOff;
    private void OnEnable()
    {
        OnPauseOn.Invoke();
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
        GameManager.Instance.ReloadScene();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        OnPauseOff.Invoke();
    }
}
