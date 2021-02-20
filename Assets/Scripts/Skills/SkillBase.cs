using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class SkillBase : MonoBehaviour
{
    public Skill_SO tmp_skillData;
    [HideInInspector]
    public Skill_SO skillData;
    public float cooldown => skillData.cooldown;
    protected Animator animator;
    protected virtual void Awake()
    {
        skillData = Instantiate(tmp_skillData);
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        skillData.cooldown -= Time.deltaTime;
    }

    public virtual void UpgradeSkill()
    {
        if (skillData.level == skillData.maxLevel) return;

        skillData.level += 1;
        skillData.cooldown *= 0.8f;
        skillData.attack = Mathf.FloorToInt(skillData.attack * 1.5f);
        skillData.range = Mathf.FloorToInt(skillData.range * 1.5f);
    }
    public abstract void Excute(IDamage target);
    public abstract void Excute(Vector3 position);
    public abstract void Interrupt();
}
