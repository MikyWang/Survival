using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnGroundState : MouseStateBase
{
    protected override Tag id => Tag.Ground;
    bool isMoveClicked;
    GameManager gameManager => GameManager.Instance;
    public MouseOnGroundState() : base() { }
    public override void Hover()
    {
        if (isMoveClicked) return;
        base.Hover();
    }
    public override void LeftClick(ref RaycastHit hit)
    {
    }
    public override void RightClick(ref RaycastHit hit)
    {
        gameManager.StartCoroutine(SetMoveCursor());
        var position = hit.point;
        gameManager.CallSelectedPlayerDoWork((player) =>
        {
            if (player.selectedObject.TryGetComponent<PlayerController>(out var controller))
            {
                controller.Move(position);
            }
        });
    }
    IEnumerator SetMoveCursor()
    {
        isMoveClicked = true;
        Cursor.SetCursor(cursorData.icons[1], new Vector2(0, 0), CursorMode.Auto);
        yield return new WaitForSeconds(0.1f);
        isMoveClicked = false;
    }
}