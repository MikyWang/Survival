using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamage
{
    public Transform headUITransform;
    public SkillId[] skillIds;
    public LiveStats stats { get; protected set; }
    public abstract float speed { get; set; }
    public abstract bool isHitting { get; set; }
    public abstract bool isDizzying { get; set; }
    public abstract bool isThinking { get; set; }
    public GameObject self => gameObject;
    public bool isDead => stats.isDead;
    public abstract void Move(Vector3 target);
    public abstract void Attack();
    public abstract void TakingDamage(int damage);
    public abstract void TakingHit(int damage, float time);
    public abstract IEnumerator TakingDizzy(float time);
    public abstract void RecoverHP(int point);
    public Dictionary<SkillId, SkillBase> skills;

    protected virtual void Awake()
    {
        stats = GetComponent<LiveStats>();
    }
    protected virtual void Start()
    {
        InitSkills();
        if (headUITransform != null)
        {
            HealthBarManager.Instance.RegisterController(this);
        }
    }
    protected virtual void Update()
    {
        if (isDead) return;
    }
    protected virtual void LateUpdate()
    {
        if (isDead) return;
    }
    public virtual void InitSkills()
    {
        skills = new Dictionary<SkillId, SkillBase>();
        foreach (var skillId in skillIds)
        {
            var skillComp = Type.GetType(skillId.ToString());
            var skill = gameObject.AddComponent(skillComp);

            skills.Add(skillId, skill as SkillBase);
        }
    }
}


