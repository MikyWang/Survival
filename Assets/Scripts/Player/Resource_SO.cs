using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_resourceData", menuName = "数据/创建新资源数据")]
public class Resource_SO : ScriptableObject
{
    public ResourceId resourceId;
    public string resourceName;
    public int amount;
    [TextArea] public string description;
}
