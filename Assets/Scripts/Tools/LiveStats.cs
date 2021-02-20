using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LiveStats : MonoBehaviour
{
    public Live_SO tmp_LiveData;

    [HideInInspector]
    public Live_SO liveData;

    private void Awake()
    {
        liveData = Instantiate(tmp_LiveData);
    }

    private void Update()
    {
        liveData.cooldown -= Time.deltaTime;
        liveData.attackSpeed -= Time.deltaTime;
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
    public bool CheckAttack()
    {
        if (attackSpeed <= 0)
        {
            liveData.attackSpeed = tmp_LiveData.attackSpeed;
            return true;
        }
        return false;

    }

}
