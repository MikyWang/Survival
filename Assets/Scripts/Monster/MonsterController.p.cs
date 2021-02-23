using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController
{

    public override float speed
    {
        get => animator.GetFloat(AnimationHash.speedHash);
        set => animator.SetFloat(AnimationHash.speedHash, value);
    }

    public bool isWalking
    {
        get => animator.GetBool(AnimationHash.isWalking);
        set
        {

            animator.SetBool(AnimationHash.isWalking, value);
        }
    }
    public bool isChasing
    {
        get => animator.GetBool(AnimationHash.isChasing);
        set => animator.SetBool(AnimationHash.isChasing, value);
    }
    public bool isDefensing
    {
        get => animator.GetBool(AnimationHash.isDefensing);
        set => animator.SetBool(AnimationHash.isDefensing, value);
    }
    public override bool isHitting { get; set; }
    public override bool isThinking { get; set; }

    public override bool isDizzying
    {
        get => animator.GetBool(AnimationHash.isDizzying);
        set
        {
            animator.SetBool(AnimationHash.isDizzying, value);
        }

    }
    public bool isDead
    {
        get => animator.GetBool(AnimationHash.isDead);
        set => animator.SetBool(AnimationHash.isDead, value);
    }

    public override bool useSkill
    {
        get => animator.GetBool(AnimationHash.useSkill);
        set => animator.SetBool(AnimationHash.useSkill, value);
    }


}
