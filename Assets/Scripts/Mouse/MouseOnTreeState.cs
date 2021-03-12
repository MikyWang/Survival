using UnityEngine;

public class MouseOnTreeState : MouseStateBase
{
    public MouseOnTreeState(Tag tag, Texture2D icon) : base(tag, icon) { }
    public override void OnStateLeftClick()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateRightClick()
    {
        throw new System.NotImplementedException();
    }
}
