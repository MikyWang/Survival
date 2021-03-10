using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : SkillBase
{
    public override SkillId id => SkillId.Build;
    public override int distance => skillData.distance;
    public bool isBuilding { get; set; }
    public override int damage => skillData.attack;

    public override void Excute(IDamage target)
    {
        if (target.self.TryGetComponent<Building>(out var building))
        {
            if (building.isFinished) return;

            this.target = target;
            skillData.distance = building.radius;
            skillData.attack = building.progressEveryTime;
            building.OnBuildingFinished += Interrupt;
        }
        StartCoroutine(BuildBuilding());
    }
    public override void Excute()
    {
        BuildingUIManager.Instance.ShowPanel();
    }

    public override void Excute(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public override void Interrupt()
    {
        target = null;
        if (isBuilding)
        {
            animator.SetTrigger(AnimationHash.endAttack);
        }
        StopAllCoroutines();
    }

    IEnumerator BuildBuilding()
    {
        if (target == null) yield break;

        yield return MoveToTarget(() =>
        {
            if (isBuilding)
                animator.SetTrigger(AnimationHash.endAttack);
        });
        transform.LookAt(target.self.transform);
        while (!CheckSkill())
        {
            yield return null;
        }
        if (target != null)
        {
            animator.SetTrigger(AnimationHash.build);
            yield return BuildBuilding();
        }
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    void BuildUp()
    {
        if (target == null)
        {
            Interrupt();
            return;
        }
        if (target != null && target.self.TryGetComponent<Building>(out var building))
        {
            if (!building.isFinished)
            {
                building.GlowUp();
            }
        }
    }
}
