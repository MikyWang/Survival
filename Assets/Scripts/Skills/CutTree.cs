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

        var agent = GetComponent<NavMeshAgent>();
        while (Vector3.Distance(transform.position, target.defender.transform.position) > skillData.distance)
        {
            agent.destination = target.defender.transform.position;
            yield return null;
        }
        agent.destination = transform.position;
        transform.LookAt(target.defender.transform);
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
        target?.defender.transform.LookAt(transform);
        target?.TakingHit();
    }

}
