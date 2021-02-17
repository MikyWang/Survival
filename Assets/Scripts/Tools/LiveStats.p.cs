using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LiveStats
{
    public int attack => liveData.attack;
    public int attackRange => liveData.attackRange;
    public int defense => liveData.defense;
    public float attackSpeed => liveData.attackSpeed;
    public float cooldown => liveData.cooldown;
    public int skillRange => liveData.skillRange;
}
