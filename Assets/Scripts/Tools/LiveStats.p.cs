using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LiveStats
{
    public int attack => liveData.attack;
    public int attackRange => liveData.attackRange;
    public int defense => liveData.defense;
    public float cooldown => liveData.cooldown;
    public int health => liveData.health;
    public int maxHealth => liveData.maxHealth;
    public int level => liveData.level;
    public int levelPoint => liveData.levelPoint;
    public int maxLevelPoint => liveData.maxLevelPoint;
    public bool isDead => health <= 0;
    public int deadPoint => liveData.deadPoint;
}
