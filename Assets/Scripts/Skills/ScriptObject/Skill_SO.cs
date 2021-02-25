using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "_skillData", menuName = "数据/新建技能")]
public class Skill_SO : ScriptableObject
{
    public SkillId skillId;
    public MonoScript skillComp;
    public SkillType skillType;
    public string skillName;
    public float cooldown;
    public int attack;
    public int level;
    public int maxLevel;
    public int distance;//使用距离
    public int range;//作用范围
    [TextArea]
    public string description;
}
