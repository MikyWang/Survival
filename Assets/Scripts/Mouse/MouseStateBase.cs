using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MouseStateBase : IMouseState
{
    protected Tag _tag;
    protected Texture2D _icon;
    public string tag => _tag.ToString();
    public MouseStateBase(Tag tag, Texture2D icon)
    {
        _tag = tag;
        _icon = icon;
    }
    public virtual void OnStateHover()
    {
        Cursor.SetCursor(_icon, new Vector2(0, 0), CursorMode.Auto);
    }
    public abstract void OnStateLeftClick();
    public abstract void OnStateRightClick();
}
