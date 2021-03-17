using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    [Header("基本数据")]
    [SerializeField] List<SOData<LiveId, Live_SO>> basicDataList;
    [Header("怪物生成")]
    [SerializeField] List<SOData<MonsterId, MonsterSpawner_SO>> spawnDataList;
    [Header("技能数据")]
    [SerializeField] List<SOData<SkillId, Skill_SO>> skillDataList;
    [Header("资源数据")]
    [SerializeField] List<SOData<ResourceId, Resource_SO>> resourceDataList;
    [Header("建筑数据")]
    [SerializeField] List<SOData<BuildingId, Building_SO>> buildingDataList;
    [Header("鼠标指针")]
    [SerializeField] List<SOData<Tag, Cursor_SO>> cursorDataList;
    public Dictionary<LiveId, Live_SO> basicDataDic { get; private set; }
    public Dictionary<MonsterId, MonsterSpawner_SO> spawnDataDic { get; private set; }
    public Dictionary<SkillId, Skill_SO> skillDataDic { get; private set; }
    public Dictionary<ResourceId, Resource_SO> resourceDataDic { get; private set; }
    public Dictionary<BuildingId, Building_SO> buildingDataDic { get; private set; }
    public Dictionary<Tag, Cursor_SO> cursorDataDic { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        InitDictionary();
        DontDestroyOnLoad(this);
    }
    public void InitDictionary()
    {
        basicDataDic = InitDictionaryInternal(basicDataList);
        spawnDataDic = InitDictionaryInternal(spawnDataList);
        skillDataDic = InitDictionaryInternal(skillDataList);
        resourceDataDic = InitDictionaryInternal(resourceDataList);
        buildingDataDic = InitDictionaryInternal(buildingDataList);
        cursorDataDic = InitDictionaryInternal(cursorDataList);
    }
    Dictionary<TId, TSO> InitDictionaryInternal<TId, TSO>(List<SOData<TId, TSO>> dataList)
    {
        var dictionary = new Dictionary<TId, TSO>();
        foreach (var data in dataList)
        {
            if (!dictionary.ContainsKey(data.id))
            {
                dictionary.Add(data.id, data.so);
            }
        }
        return dictionary;
    }

}
[System.Serializable]
public class SOData<TId, TSO>
{
    public TId id;
    public TSO so;
}