using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "_buildingData", menuName = "数据/新建建筑数据")]
public class Building_SO : ScriptableObject
{
    public BuildingId id;
    public string buildingName;
    public int buildingProgress;
    public int radius;
    public int woodCost;
    public int goldCost;
    public int progressEveryTime;
    public bool canUpgrade;
    public BuildingId upgradeBuilding;
    [TextArea] public string description;
}
