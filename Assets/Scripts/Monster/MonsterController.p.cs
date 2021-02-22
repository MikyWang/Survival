using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController
{

    public override float speed { get => agent.speed; set => agent.speed = value; }

    public bool isWalking
    {
        get => animator.GetBool(AnimationHash._isWalking);
        set
        {

            animator.SetBool(AnimationHash._isWalking, value);
        }
    }
    public bool isChasing
    {
        get => animator.GetBool(AnimationHash._isChasing);
        set => animator.SetBool(AnimationHash._isChasing, value);
    }
    public bool isDefensing
    {
        get => animator.GetBool(AnimationHash._isDefensing);
        set => animator.SetBool(AnimationHash._isDefensing, value);
    }
    public override bool isHitting { get; set; }
    public override bool isThinking { get; set; }

    public override bool isDizzying
    {
        get => animator.GetBool(AnimationHash._isDizzying);
        set
        {
            animator.SetBool(AnimationHash._isDizzying, value);
        }

    }
    public bool isDead
    {
        get => animator.GetBool(AnimationHash._isDead);
        set => animator.SetBool(AnimationHash._isDead, value);
    }

    public override bool useSkill
    {
        get => animator.GetBool(AnimationHash._useSkill);
        set => animator.SetBool(AnimationHash._useSkill, value);
    }


}
