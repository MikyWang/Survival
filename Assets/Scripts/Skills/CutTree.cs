using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutTree : SkillBase
{
    public IDamage target { get; set; }
    public bool isCutting { get; set; }
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
            Excute(target);
        }
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
    IEnumerator Cut()
    {
        if (target == null) yield break;

        yield return MoveToTarget(target.self.transform);
        transform.LookAt(target.self.transform);
        if (CheckSkill())
        {
            animator.SetTrigger(AnimationHash.cutTree);
        }
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    void CutDownTree()
    {
        target?.self.transform.LookAt(transform);
        target?.TakingHit(0, 0);
    }

}
