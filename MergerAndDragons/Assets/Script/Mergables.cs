using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Mergables", order = 1)]
public class Mergables : ScriptableObject
{
    public string name;
    public Sprite image;
    public Mergables mergeOutcome;
    public int itemCode;
    public float currencyPerTick;
    public string itemDescription = "This is a temporary descrition of this item";
}
