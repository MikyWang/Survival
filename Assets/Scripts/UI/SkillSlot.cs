using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IObserver<SkillBase>
{
    [SerializeField] Image iconImg;
    [SerializeField] Image cooldownImg;
    [SerializeField] TMP_Text cooldownText;
    [SerializeField] TMP_Text skillName;
    IDisposable unsubscribe;
    int userId;
    SkillBase bindSkill;
    public void InitSkillUI(SkillBase skill)
    {
        var id = skill.gameObject.GetInstanceID();
        if (userId != id)
        {
            userId = id;
            unsubscribe?.Dispose();
            unsubscribe = skill.Subscribe(this);
            bindSkill = skill;
        }
        iconImg.sprite = skill.icon;
        skillName.text = skill.skillName;
    }
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(SkillBase skill)
    {
        cooldownImg.fillAmount = skill.cooldownPercent;
        if (skill.cooldown <= 0)
        {
            cooldownText.gameObject.SetActive(false);
        }
        else
        {
            cooldownText.text = $"{skill.cooldown.ToString("N1")}s";
            cooldownText.gameObject.SetActive(true);
        }
    }

    public void Excute()
    {
        bindSkill.Excute();
    }
}
