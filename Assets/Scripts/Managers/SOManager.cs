using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    [Header("基本数据")]
    public Live_SO peasantBasicData;

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
