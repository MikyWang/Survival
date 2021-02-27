using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutTree : SkillBase
{
    public IDamage target { get; set; }
    public bool isCutting { get; set; }
    public override SkillId id => SkillId.CutTree;
    public override void Excute(IDamage target)
    {
        this.target = target;
        StartCoroutine(Cut());
    }

    override protected void Update()
    {
        base.Update();
        if (target != null)
        {
            if (target.isDead)
            {
                Interrupt();
                return;
            }
            if (!isCutting)
            {
                Excute(target);
            }
        }
    }

    public override void Excute(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
    public override void Interrupt()
    {
        target = null;
        animator.SetTrigger(AnimationHash.endAttack);
        StopAllCoroutines();
    }
    IEnumerator Cut()
    {
        if (target == null) yield break;

        yield return MoveToTarget(target.self.transform, () =>
        {
            animator.SetTrigger(AnimationHash.endAttack);
        });
        if (CheckSkill())
        {
            transform.LookAt(target.self.transform);
            animator.SetTrigger(AnimationHash.cutTree);
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
