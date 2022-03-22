using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunChoiceUI : MonoBehaviour
{
    public Color defaultColor;
    public Color selectColor;

    public Image bg;
    public TextMeshProUGUI text;

    public void Init(int num)
    {
        ResetColor();

        text.text = (num+1).ToString();
    }

    public void Select()
    {
        bg.color = selectColor;
        text.color = selectColor;
    }

    public void ResetColor()
    {
        bg.color = defaultColor;
        text.color = defaultColor;
    }
}
