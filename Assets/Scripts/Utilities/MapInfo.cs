using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapInfo", fileName = "MapInfo")]
public class MapInfo : ScriptableObject
{
    public string name = "";

    public int round = 0;
}
