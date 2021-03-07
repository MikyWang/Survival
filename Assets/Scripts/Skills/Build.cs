using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : SkillBase
{
    public override SkillId id => SkillId.Build;
    public override int distance => skillData.distance;
    public override int damage => skillData.attack;

    public override void Excute(IDamage target)
    {
        if (target.self.TryGetComponent<Building>(out var building))
        {
            skillData.distance = building.radius;
            skillData.attack = building.progressEveryTime;
        }
    }

    public override void Excute(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public override void Interrupt()
    {
        throw new System.NotImplementedException();
    }
}
