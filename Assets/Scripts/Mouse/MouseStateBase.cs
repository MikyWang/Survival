using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MouseStateBase : IMouseState
{
    protected Cursor_SO cursorData;
    protected GameManager gameManager => GameManager.Instance;
    protected abstract Tag id { get; }
    public string tag => id.ToString();
    public MouseStateBase()
    {
        cursorData = GameObject.Instantiate(SOManager.Instance.cursorDataDic[id]);
    }
    public virtual void Hover()
    {
        Cursor.SetCursor(cursorData.icons[0], new Vector2(0, 0), CursorMode.Auto);
    }
    public abstract void LeftClick(ref RaycastHit hit);
    public abstract void RightClick(ref RaycastHit hit);
}
