using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(LiveStats))]
public abstract class SkillBase : MonoBehaviour
{
    public abstract SkillId id { get; }
    [HideInInspector]
    public Skill_SO skillData;
    public float cooldown => stats.cooldown + skillData.cooldown;
    public float skillDistance => stats.attackRange + skillData.distance;
    protected Animator animator;
    protected LiveStats stats;
    protected Skill_SO tmp_skillData => SOManager.Instance.skillDataDic[id];
    protected virtual void Awake()
    {
        skillData = Instantiate(tmp_skillData);
        animator = GetComponent<Animator>();
        stats = GetComponent<LiveStats>();
    }
    protected virtual void Update()
    {
        skillData.cooldown -= Time.deltaTime;
    }

    public virtual bool CheckSkill()
    {
        if (cooldown <= 0)
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

    protected IEnumerator MoveToTarget(Transform target, Action OnRoad = null)
    {
        var agent = GetComponent<NavMeshAgent>();
        while (Vector3.Distance(transform.position, target.position) > skillDistance)
        {
            agent.destination = target.position;
            OnRoad?.Invoke();
            yield return null;
        }
        agent.destination = transform.position;
    }

    public abstract void Excute(IDamage target);
    public abstract void Excute(Vector3 position);
    public abstract void Interrupt();
}
