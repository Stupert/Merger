using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> cellData = new List<string>();
    public double epochTime;
    public int energy;
    public int adCount;
    public double adRefreshDate;

    public PlayerData(List<string> playerCells, double time, int savedEnergy, int savedAdCount, double savedAdRefreshDate)
    {
        cellData = playerCells;
        epochTime = time;
        energy = savedEnergy;
        adCount = savedAdCount;
        adRefreshDate = savedAdRefreshDate;
    }
}