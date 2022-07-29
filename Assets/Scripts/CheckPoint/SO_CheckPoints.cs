using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/CheckPoints", fileName = "CheckPoints")]
public class SO_CheckPoints : ScriptableObject
{
    public List<int> unlokeds;
}
