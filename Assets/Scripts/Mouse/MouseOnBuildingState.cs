using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnBuildingState : MouseStateBase
{
    protected override Tag id => Tag.Building;

    public override void LeftClick(ref RaycastHit hit)
    {

    }

    public override void RightClick(ref RaycastHit hit)
    {
    }
}
