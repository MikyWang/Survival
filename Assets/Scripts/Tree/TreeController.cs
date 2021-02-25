using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : ControllerBase
{
    private Animator animator;
    public override float speed { get => 0; set { } }
    public override bool isHitting { get; set; }
    public override bool isDizzying { get => false; set { } }
    public override bool isThinking { get => false; set { } }
    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    public override void RecoverHP(int point)
    {
        throw new System.NotImplementedException();
    }

    public override void TakingDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator TakingDizzy(float time)
    {
        yield break;
    }

    public override void TakingHit(int damage, float time)
    {
        animator.SetTrigger(AnimationHash.getHit);
    }

}
