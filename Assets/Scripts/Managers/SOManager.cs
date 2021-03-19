using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    [Header("基本数据")]
    [SerializeField] SOStorage<LiveId, Live_SO> _basicDataDic;
    [Header("怪物生成")]
    [SerializeField] SOStorage<MonsterId, MonsterSpawner_SO> _spawnDataDic;
    [Header("技能数据")]
    [SerializeField] SOStorage<SkillId, Skill_SO> _skillDataDic;
    [Header("资源数据")]
    [SerializeField] SOStorage<ResourceId, Resource_SO> _resourceDataDic;
    [Header("建筑数据")]
    [SerializeField] SOStorage<BuildingId, Building_SO> _buildingDataDic;
    [Header("鼠标指针")]
    [SerializeField] SOStorage<Tag, Cursor_SO> _cursorDataDic;

    public SOStorage<LiveId, Live_SO> basicDataDic => _basicDataDic;
    public SOStorage<MonsterId, MonsterSpawner_SO> spawnDataDic => _spawnDataDic;
    public SOStorage<SkillId, Skill_SO> skillDataDic => _skillDataDic;
    public SOStorage<ResourceId, Resource_SO> resourceDataDic => _resourceDataDic;
    public SOStorage<BuildingId, Building_SO> buildingDataDic => _buildingDataDic;
    public SOStorage<Tag, Cursor_SO> cursorDataDic => _cursorDataDic;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
