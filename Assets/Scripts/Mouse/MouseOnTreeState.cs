using UnityEngine;

public class MouseOnTreeState : MouseStateBase
{
    protected override Tag id => Tag.Tree;
    GameManager gameManager => GameManager.Instance;
    public MouseOnTreeState() : base() { }
    public override void LeftClick(ref RaycastHit hit)
    {
    }

    public override void RightClick(ref RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<TreeController>(out var tree))
        {
            gameManager.CallSelectedPlayerDoWork(player =>
                  {
                      if (player.selectedObject.TryGetComponent<Peasant>(out var peasant))
                      {
                          peasant.CutTree(tree);
                      }
                  });
        }
    }
}
