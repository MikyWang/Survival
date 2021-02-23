using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : SkillBase
{
    public IDamage target { get; set; }
    public override void Excute(IDamage target)
    {
        this.target = target;
        StartCoroutine(MoveAndAttack());
    }

    public override void Excute(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public override void Interrupt()
    {
        target = null;
        StopAllCoroutines();
    }

    IEnumerator MoveAndAttack()
    {
        if (target == null) yield break;

        yield return MoveToTarget(target.self.transform);
        transform.LookAt(target.self.transform);
        if (CheckSkill())
        {
            animator.SetTrigger(AnimationHash.attack);
        }
    }
    /// <summary>
    /// 动画事件
    /// </summary>
    private void AttackTarget()
    {
        target?.self.GetComponent<IDamage>().TakingDamage(stats.attack);
    }
}
