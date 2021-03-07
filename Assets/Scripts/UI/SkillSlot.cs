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
    public void InitSkillUI(SkillBase skill)
    {
        var id = skill.gameObject.GetInstanceID();
        if (userId != id)
        {
            userId = id;
            unsubscribe?.Dispose();
            unsubscribe = skill.Subscribe(this);
        }
        iconImg.sprite = skill.icon;
        skillName.text = skill.skillName;
    }
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(SkillBase skill)
    {
        cooldownImg.fillAmount = skill.cooldownPercent;
        cooldownText.text = $"{Mathf.Max(0, skill.cooldown).ToString("N1")}s";
    }
}
