using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public string[] spawnerNames;
    public List<Transform> spawnPoints;
    private List<MonsterSpawner_SO> spawnerDataList;

    private void Awake()
    {
        InitialData();
    }
    private void LateUpdate()
    {
        SpawnMonsters();
    }

    public void SpawnMonsters()
    {
        foreach (var monster in spawnerDataList)
        {
            monster.remainingTime -= Time.deltaTime;
            if (monster.remainingTime < 0)
            {
                monster.remainingTime = monster.period;
                StartCoroutine(SpawnMonster(monster));
            }
        }
    }

    IEnumerator SpawnMonster(MonsterSpawner_SO monster)
    {
        for (var i = 0; i < monster.count; i++)
        {
            var index = Random.Range(0, 4);
            var transform = spawnPoints[index];
            Instantiate(monster.monsterPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(1);
        }
    }

    void InitialData()
    {
        spawnerDataList = new List<MonsterSpawner_SO>();
        foreach (var name in spawnerNames)
        {
            var spawn = Instantiate(SOManager.Instance.spawnDataDic[name]);
            spawnerDataList.Add(spawn);
        }
    }

}
