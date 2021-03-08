using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Building : BuildingBase
{
    Building_SO buildingStats;
    public string buildingName => buildingStats.buildingName;
    public int radius => buildingStats.radius;
    public int woodCost => buildingStats.woodCost;
    public int goldCost => buildingStats.goldCost;
    public int progressEveryTime => buildingStats.progressEveryTime;
    public bool canUpgrade => buildingStats.canUpgrade;
    public BuildingId upgradeBuilding => buildingStats.upgradeBuilding;
    public string description => buildingStats.description;
    public bool isFinished => buildingProgress >= 100;
    public int buildingProgress
    {
        get => buildingStats.buildingProgress;
        set
        {
            buildingStats.buildingProgress = value;
            Notify();
        }
    }


}
