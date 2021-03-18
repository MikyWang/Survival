using UnityEngine;

public class MouseOnTreeState : MouseStateBase
{
    protected override Tag id => Tag.Tree;
    public override void LeftClick(ref RaycastHit hit)
    {
    }

    public override void RightClick(ref RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<TreeController>(out var tree))
        {
            gameManager.CallSelectedPlayerDoWork<Peasant>(peasant =>
            {
                peasant.CutTree(tree);
            });
        }
    }
}
