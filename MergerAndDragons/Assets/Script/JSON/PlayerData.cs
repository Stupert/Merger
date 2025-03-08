using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> cellData = new List<string>();

    public PlayerData(List<string> playerCells)
    {
        cellData = playerCells;
    }
}