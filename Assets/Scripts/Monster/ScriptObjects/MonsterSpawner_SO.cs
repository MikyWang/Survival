using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_spawnData", menuName = "数据/创建怪物生成数据")]
public class MonsterSpawner_SO : ScriptableObject
{
    public GameObject monsterPrefab;
    public float remainingTime;
    public float period;
    public float count;
}
