using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : Singleton<SkillUIManager>
{
    [SerializeField] SkillSlot skillSlotPrefab;
    [SerializeField] GameObject skillScroll;
    [SerializeField] GameObject avatarImg;
    [SerializeField] GameObject avatarCamera;
    List<ISelected> selectedPlayers => GameManager.Instance.selectedPlayers;
    Dictionary<SkillId, SkillSlot> slots = new Dictionary<SkillId, SkillSlot>();
    Vector3 cameraLocalPos;
    Quaternion cameraLocalRot;
    override protected void Awake()
    {
        base.Awake();
        KeepAvatarCamTransform();
    }
    public void UpdateSkillUI()
    {
        if (selectedPlayers.Count <= 0)
        {
            HideSkillUI();
            return;
        }
        InitAllSkills();
        UpdateSlot();
        SetAvatarCameraPosition();
        ShowSkillUI();
    }
    void KeepAvatarCamTransform()
    {
        cameraLocalPos = avatarCamera.transform.position;
        cameraLocalRot = avatarCamera.transform.rotation;
    }
    void HideSkillUI()
    {
        skillScroll.SetActive(false);
        avatarImg.SetActive(false);
        avatarCamera.gameObject.SetActive(false);
    }
    void ShowSkillUI()
    {
        skillScroll.SetActive(true);
        skillScroll.transform.DOMoveY(0, 0.2f).From().SetEase(Ease.Linear);
        avatarImg.SetActive(true);
        avatarImg.transform.DOMoveY(0, 0.2f).From().SetEase(Ease.Linear);
        avatarCamera.gameObject.SetActive(true);
    }
    void SetAvatarCameraPosition()
    {
        foreach (var player in selectedPlayers)
        {
            if (player.selectedObject.TryGetComponent<ControllerBase>(out var controller))
            {
                avatarCamera.transform.parent = controller.headUITransform;
                avatarCamera.transform.localPosition = cameraLocalPos;
                avatarCamera.transform.localRotation = cameraLocalRot;
                break;
            }
        }
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
