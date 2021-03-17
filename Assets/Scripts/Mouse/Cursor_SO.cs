using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_cursorData", menuName = "数据/创建新鼠标指针")]
public class Cursor_SO : ScriptableObject
{
    public Tag id;
    public string tagName;
    public List<Texture2D> icons;
    [TextArea] public string description;
}
