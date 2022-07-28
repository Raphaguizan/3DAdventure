using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Save;
public class SaveUIFeedBack : MonoBehaviour
{
    [SerializeField]
    private float _showTime = 3f;

    public GameObject UISaveFeedBack;
    public GameObject UILoadFeedBack;

    private void Start()
    {
        if (UILoadFeedBack) UILoadFeedBack.SetActive(false);
        if (UISaveFeedBack) UISaveFeedBack.SetActive(false);

        SaveManager.Loaded += ShowLoad;
        SaveManager.ToSave += ShowSave;
    }

    private void ShowLoad(SaveSetUp setup)
    {
        if(UILoadFeedBack)
            StartCoroutine(ShowTime(UILoadFeedBack));
    }

    private void ShowSave()
    {
        if (UISaveFeedBack)
            StartCoroutine(ShowTime(UISaveFeedBack));
    }

    IEnumerator ShowTime(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(_showTime);
        obj.SetActive(false);
    }
}
