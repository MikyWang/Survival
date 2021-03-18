using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnPlayerState : MouseStateBase
{
    protected override Tag id => Tag.Player;

    public override void LeftClick(ref RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<ISelected>(out var player))
        {
            gameManager.ToggleSelector(player);
        }
    }
    public override void RightClick(ref RaycastHit hit)
    {
    }
}
