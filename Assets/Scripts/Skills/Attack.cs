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
            animator.SetTrigger(AnimationHash.attack);
        }
    }
    /// <summary>
    /// 动画事件
    /// </summary>
    private void AttackTarget()
    {
        target?.defender.GetComponent<IDamage>().TakingDamage(stats.attack);
    }
}
