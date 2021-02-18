using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelected
{
    /// <summary>
    /// 是否被选中
    /// </summary>
    /// <value></value>
    public bool isSelected { get; }
    public GameObject selectedObject { get; }
    /// <summary>
    /// 使用prefab高亮自身
    /// </summary>
    /// <param name="highlightingPrefab">高亮样式</param>
    public void Select(GameObject highlightingPrefab);
    /// <summary>
    /// 取消高亮
    /// </summary>
    public void UnSelect();
}
