using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    [Header("基本数据")]
    [SerializeField]
    private List<Live_SOData> basicDataList;
    [Header("怪物生成")]
    [SerializeField]
    private List<MonsterSpawn_SOData> spawnDataList;
    [Header("技能数据")]
    [SerializeField]
    private List<Skill_SOData> skillDataList;
    public Dictionary<LiveId, Live_SO> basicDataDic { get; private set; }
    public Dictionary<MonsterId, MonsterSpawner_SO> spawnDataDic { get; private set; }
    public Dictionary<SkillId, Skill_SO> skillDataDic { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        InitDictionary();
        DontDestroyOnLoad(this);
    }
    private void InitDictionary()
    {
        basicDataDic = new Dictionary<LiveId, Live_SO>();
        spawnDataDic = new Dictionary<MonsterId, MonsterSpawner_SO>();
        skillDataDic = new Dictionary<SkillId, Skill_SO>();
        foreach (var data in basicDataList)
        {
            if (!basicDataDic.ContainsKey(data.id))
            {
                basicDataDic.Add(data.id, data.so);
            }
        }
        foreach (var data in spawnDataList)
        {
            if (!spawnDataDic.ContainsKey(data.id))
            {
                spawnDataDic.Add(data.id, data.so as MonsterSpawner_SO);
            }
        }
        foreach (var data in skillDataList)
        {
            if (!skillDataDic.ContainsKey(data.id))
            {
                skillDataDic.Add(data.id, data.so as Skill_SO);
            }
        }
    }

}
[System.Serializable]
public class MonsterSpawn_SOData
{
    public MonsterId id;
    public MonsterSpawner_SO so;
}

[System.Serializable]
public class Live_SOData
{
    public LiveId id;
    public Live_SO so;
}
[System.Serializable]
public class Skill_SOData
{
    public SkillId id;
    public Skill_SO so;
}