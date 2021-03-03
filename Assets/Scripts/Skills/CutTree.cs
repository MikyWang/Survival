using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutTree : SkillBase
{
    public bool isCutting { get; set; }
    public override SkillId id => SkillId.CutTree;
    public override void Excute(IDamage target)
    {
        this.target = target;
        StartCoroutine(Cut());
    }

    public override void Excute(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
    public override void Interrupt()
    {
        target = null;
        if (isCutting)
        {
            animator.SetTrigger(AnimationHash.endAttack);
        }
        StopAllCoroutines();
    }
    IEnumerator Cut()
    {
        if (target == null) yield break;

        yield return MoveToTarget(() =>
        {
            if (isCutting)
                animator.SetTrigger(AnimationHash.endAttack);
        });
        while (!CheckSkill())
        {
            yield return null;
        }
        transform.LookAt(target.self.transform);
        while (target != null)
        {
            animator.SetTrigger(AnimationHash.cutTree);
            yield return Cut();
        }

    }

    /// <summary>
    /// 动画事件
    /// </summary>
    void CutDownTree()
    {
        if (target == null) return;
        if (Vector3.Distance(transform.position, target.self.transform.position) > skillDistance) return;

        target.self.transform.LookAt(transform);
        target.TakingHit(controller, damage, 0);
    }

}
