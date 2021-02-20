using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutTree : SkillBase
{
    public IDamage target { get; set; }
    public bool isCutting { get => animator.GetBool(AnimationHash._isCutting); set => animator.SetBool(AnimationHash._isCutting, value); }

    public override void Excute(IDamage target)
    {
        isCutting = false;
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
        isCutting = false;
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
        isCutting = true;
    }

}
