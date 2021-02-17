using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_spawnData", menuName = "怪物生成/创建新数据")]
public class MonsterSpawner_SO : ScriptableObject
{
    public GameObject monsterPrefab;
    public float remainingTime;
    public float period;
    public float count;
}
