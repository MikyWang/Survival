using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_basicData", menuName = "数据/创建新基本数据")]
public class Live_SO : ScriptableObject
{
    public string liveName; //生物名字
    public int health; //当前血量
    public int maxHealth; //最大血量
    public int attack; //基础攻击力
    public int defense; //基础防御力
    public float attackSpeed; //攻击速度
    public float cooldown; //技能冷却时间
    public int attackRange;//攻击距离
    public int skillRange; //技能距离
    public int level; //等级
    public int levelPoint; //经验值
    public int maxLevelPoint; //最大经验值
    public int growthAttackPoint; //成长攻击力
    public int growthHealthPoint; //成长血量
    public int growthDefensePoint; //成长防御力
    public int deadPoint; //死亡掉落经验值
    [TextArea]
    public string description; //说明
}
