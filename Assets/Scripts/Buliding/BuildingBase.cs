using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : ControllerBase
{
    public override float speed { get; set; }
    public override bool isHitting { get; set; }
    public override bool isDizzying { get; set; }
    public override bool isThinking { get; set; }

    public override void Attack()
    {
    }

    public override void Move(Vector3 target)
    {
    }

    public override void RecoverHP(int point)
    {
    }

    public override void TakingDamage(ControllerBase attacker, int damage)
    {
    }

    public override IEnumerator TakingDizzy(float time)
    {
        yield break;
    }

    public override void TakingHit(ControllerBase attacker, int damage, float time)
    {
    }
}
