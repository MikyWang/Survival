using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(LiveStats))]
public abstract partial class SkillBase : MonoBehaviour
{
    public abstract SkillId id { get; }
    [HideInInspector]
    public Skill_SO skillData;
    public IDamage target { get; set; }
    public float cooldown => stats.cooldown + skillData.cooldown;
    public virtual int distance => stats.attackRange + skillData.distance;
    public virtual int damage => stats.attack + skillData.attack;
    public Sprite icon => skillData.icon;
    public string skillName => skillData.skillName;
    public float cooldownPercent => (cooldown / (SOManager.Instance.basicDataDic[stats.id].cooldown + tmp_skillData.cooldown));
    public bool canUse { get; set; } = true;
    protected Animator animator;
    protected LiveStats stats;
    protected ControllerBase controller;
    protected Skill_SO tmp_skillData => SOManager.Instance.skillDataDic[id];
    protected virtual void Awake()
    {
        skillData = Instantiate(tmp_skillData);
        animator = GetComponent<Animator>();
        stats = GetComponent<LiveStats>();
        controller = GetComponent<ControllerBase>();
        skillData.cooldown = 0;
    }
    protected virtual void Update()
    {
        skillData.cooldown -= Time.deltaTime;
        Notify();
    }

    public virtual bool CheckSkill()
    {
        if (cooldown <= 0 && canUse)
        {
            stats.CheckSkill();
            skillData.cooldown = tmp_skillData.cooldown;
            return true;
        }
        return false;
    }

    public virtual void UpgradeSkill()
    {
        if (skillData.level == skillData.maxLevel) return;

        skillData.level += 1;
        skillData.cooldown *= 0.8f;
        skillData.attack = Mathf.FloorToInt(skillData.attack * 1.5f);
        skillData.range = Mathf.FloorToInt(skillData.range * 1.5f);
    }

    protected IEnumerator MoveToTarget(Action OnRoad = null)
    {
        var agent = GetComponent<NavMeshAgent>();
        while (Vector3.Distance(transform.position, target.self.transform.position) > distance)
        {
            OnRoad?.Invoke();
            agent.destination = target.self.transform.position;
            yield return null;
        }
        agent.destination = transform.position;
    }
    public abstract void Excute();
    public abstract void Excute(IDamage target);
    public abstract void Excute(Vector3 position);
    public abstract void Interrupt();
}
