using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class LiveStats : MonoBehaviour
{
    public LiveId id;
    [HideInInspector]
    public Live_SO liveData;
    private Live_SO tmp_LiveData => SOManager.Instance.basicDataDic[id];
    private void Start()
    {
        liveData = Instantiate(tmp_LiveData);
        liveData.cooldown = 0;
    }
    private void Update()
    {
        liveData.cooldown -= Time.deltaTime;
    }

    public void TakingDamage(int damage)
    {
        var point = Mathf.Max(damage - defense, 1);
        liveData.health = Mathf.Max(liveData.health - point, 0);
    }

    public bool CheckSkill()
    {
        if (cooldown <= 0)
        {
            liveData.cooldown = tmp_LiveData.cooldown;
            return true;
        }
        return false;
    }

    public void UpdateLevelExp(int point)
    {
        liveData.levelPoint += point;
        if (liveData.levelPoint >= liveData.maxLevelPoint)
        {
            liveData.level += 1;
            liveData.levelPoint -= liveData.maxLevelPoint;
            liveData.maxLevelPoint *= 2;
            liveData.attack += liveData.growthAttackPoint;
            liveData.maxHealth += liveData.growthHealthPoint;
            liveData.defense += liveData.growthDefensePoint;
            liveData.health = maxHealth;
        }
    }
}
