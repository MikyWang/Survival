using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text buildingName;
    [SerializeField] GameObject focusItem;
    Building_SO so;
    public void OnPointerEnter(PointerEventData eventData)
    {
        focusItem.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        focusItem.SetActive(false);
    }

    public void SetUp(Building_SO so)
    {
        this.so = so;
        icon.sprite = so.icon;
        buildingName.text = so.buildingName.ToString();
    }
    public void CheckAndBuild()
    {
        if (!CheckResources()) return;
        BuildingUIManager.Instance.BuildBuilding(so);
    }

    bool CheckResources()
    {
        var woodHaves = GameManager.Instance.resources.Find(x => x.id == ResourceId.Wood).amount;
        var goldHaves = GameManager.Instance.resources.Find(x => x.id == ResourceId.Gold).amount;
        return woodHaves >= so.woodCost && goldHaves >= so.goldCost;
    }

}
