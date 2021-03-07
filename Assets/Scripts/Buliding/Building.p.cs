using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Building : MonoBehaviour
{
    Building_SO stats;
    public string buildingName => stats.buildingName;
    public int radius => stats.radius;
    public int woodCost => stats.woodCost;
    public int goldCost => stats.goldCost;
    public int progressEveryTime => stats.progressEveryTime;
    public bool canUpgrade => stats.canUpgrade;
    public BuildingId upgradeBuilding => stats.upgradeBuilding;
    public string description => stats.description;
    public int buildingProgress
    {
        get => stats.buildingProgress;
        set
        {
            stats.buildingProgress = value;
            Notify();
        }
    }

    private void Start()
    {
        stats = Instantiate(SOManager.Instance.buildingDataDic[id]);
    }

}
