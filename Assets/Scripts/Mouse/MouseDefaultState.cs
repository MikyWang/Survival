using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDefaultState : MouseStateBase
{
    protected override Tag id => Tag.Untagged;

    public override void LeftClick(ref RaycastHit hit)
    {
    }

    public override void RightClick(ref RaycastHit hit)
    {
    }
}
