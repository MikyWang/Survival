using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuildingUIManager : Singleton<BuildingUIManager>
{
    SOStorage<BuildingId, Building_SO> buildingDic => SOManager.Instance.buildingDataDic;
    [SerializeField] Transform buildingGrid;
    [SerializeField] BuildingSlot buildingSlotPrefeb;
    [SerializeField] GameObject buildingPanel;
    [SerializeField] Material buildingPreviewMat;
    bool isPlacing = false;
    Mesh buildingPreviewMesh;
    Building_SO so;
    void Start()
    {
        InitBuildingSlots();
    }

    void InitBuildingSlots()
    {
        foreach (var buildingId in buildingDic.Keys)
        {
            InitBuildingSlot(buildingDic[buildingId]);
        }
    }
    void InitBuildingSlot(Building_SO so)
    {
        var slot = Instantiate(buildingSlotPrefeb, buildingGrid);
        slot.SetUp(so);
    }

    void Update()
    {
        if (isPlacing)
        {
            var pos = Utility.MouseToTerrainPosition();
            Graphics.DrawMesh(buildingPreviewMesh, pos, Quaternion.identity, buildingPreviewMat, 0);
            if (Input.GetMouseButtonDown(0))
            {
                isPlacing = false;
                SetBuilding(pos);
                GameManager.Instance.TakeResources(so.woodCost, so.goldCost);
            }
        }
    }

    void SetBuilding(Vector3 pos)
    {
        var building = Instantiate(so.buildingPrefab, pos, Quaternion.identity).GetComponent<Building>();
    }
    public void ShowPanel()
    {
        buildingPanel.SetActive(true);
        buildingPanel.transform.DOMoveX(0, .3f).From().SetEase(Ease.Linear);
    }
    public void HidePanel()
    {
        buildingPanel.SetActive(false);
    }

    public void BuildBuilding(Building_SO building)
    {
        HidePanel();
        isPlacing = true;
        so = building;
        buildingPreviewMesh = building.buildingPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
    }
}
