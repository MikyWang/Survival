using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIManager : Singleton<SkillUIManager>
{
    [SerializeField] SkillSlot skillSlotPrefab;
    [SerializeField] GameObject skillScroll;
    List<ISelected> selectedPlayers => GameManager.Instance.selectedPlayers;
    Dictionary<SkillId, SkillSlot> slots = new Dictionary<SkillId, SkillSlot>();

    public void UpdateSkillUI()
    {
        if (selectedPlayers.Count <= 0)
        {
            skillScroll.SetActive(false);
            return;
        }
        InitAllSkills();
        UpdateSlot();
        skillScroll.SetActive(true);
    }

    void InitAllSkills()
    {
        foreach (var player in selectedPlayers)
        {
            if (player.selectedObject.TryGetComponent<ControllerBase>(out var controller))
            {
                foreach (var skillId in controller.skillIds)
                {
                    if (!slots.ContainsKey(skillId))
                    {
                        var skillSlot = Instantiate(skillSlotPrefab, skillScroll.transform.GetChild(0));
                        slots.Add(skillId, skillSlot);
                    }
                }
            }
        }
    }

    void UpdateSlot()
    {
        foreach (var skillId in slots.Keys)
        {
            var firstPlayerFound = false;
            foreach (var player in selectedPlayers)
            {
                if (firstPlayerFound)
                {
                    break;
                }
                if (player.selectedObject.TryGetComponent<ControllerBase>(out var controller))
                {
                    foreach (var id in controller.skillIds)
                    {
                        if (id == skillId && player.selectedObject.TryGetComponent(Type.GetType(skillId.ToString()), out var skill))
                        {
                            slots[skillId].InitSkillUI(skill as SkillBase);
                            slots[skillId].gameObject.SetActive(true);
                            firstPlayerFound = true;
                            break;
                        }
                        slots[skillId].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
