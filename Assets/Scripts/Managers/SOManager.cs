using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    [Header("基本数据")]
    [SerializeField]
    private List<SOData> basicDataList;
    [Header("怪物生成")]
    [SerializeField]
    private List<SOData> spawnDataList;
    [Header("技能数据")]
    [SerializeField]
    private List<SOData> skillDataList;
    public Dictionary<string, Live_SO> basicDataDic { get; private set; }
    public Dictionary<string, MonsterSpawner_SO> spawnDataDic { get; private set; }
    public Dictionary<string, Skill_SO> skillDataDic { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        InitDictionary();
        DontDestroyOnLoad(this);
    }
    private void InitDictionary()
    {
        basicDataDic = new Dictionary<string, Live_SO>();
        spawnDataDic = new Dictionary<string, MonsterSpawner_SO>();
        skillDataDic = new Dictionary<string, Skill_SO>();
        foreach (var data in basicDataList)
        {
            if (!basicDataDic.ContainsKey(data.name))
            {
                basicDataDic.Add(data.name, data.so as Live_SO);
            }
        }
        foreach (var data in spawnDataList)
        {
            if (!spawnDataDic.ContainsKey(data.name))
            {
                spawnDataDic.Add(data.name, data.so as MonsterSpawner_SO);
            }
        }
        foreach (var data in skillDataList)
        {
            if (!skillDataDic.ContainsKey(data.name))
            {
                skillDataDic.Add(data.name, data.so as Skill_SO);
            }
        }
    }

}
[System.Serializable]
public class SOData
{
    public string name;
    public ScriptableObject so;
}