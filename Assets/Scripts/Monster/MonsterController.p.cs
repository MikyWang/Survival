using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterController
{
    protected readonly int _isWalking = Animator.StringToHash("isWalking");
    protected readonly int _think = Animator.StringToHash("think");
    protected readonly int _isChasing = Animator.StringToHash("isChasing");
    protected readonly int _attack = Animator.StringToHash("attack");
    protected readonly int _getHit = Animator.StringToHash("getHit");
    protected readonly int _isDefensing = Animator.StringToHash("isDefensing");
    protected readonly int _isDizzying = Animator.StringToHash("isDizzying");
    protected readonly int _isDead = Animator.StringToHash("isDead");
    protected readonly int _useSkill = Animator.StringToHash("useSkill");
    public override float speed { get => agent.speed; set => agent.speed = value; }

    public bool isWalking
    {
        get => animator.GetBool(_isWalking);
        set
        {

            animator.SetBool(_isWalking, value);
        }
    }
    public bool isChasing
    {
        get => animator.GetBool(_isChasing);
        set => animator.SetBool(_isChasing, value);
    }
    public bool isDefensing
    {
        get => animator.GetBool(_isDefensing);
        set => animator.SetBool(_isDefensing, value);
    }
    public override bool isHitting { get; set; }
    public override bool isThinking { get; set; }

    public override bool isDizzying
    {
        get => animator.GetBool(_isDizzying);
        set
        {
            animator.SetBool(_isDizzying, value);
        }

    }
    public bool isDead
    {
        get => animator.GetBool(_isDead);
        set => animator.SetBool(_isDead, value);
    }

    public override bool useSkill
    {
        get => animator.GetBool(_useSkill);
        set => animator.SetBool(_useSkill, value);
    }


}
