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
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    protected override void Start() { }
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
        StopAllCoroutines();
        HealthBarManager.Instance.RegisterController(this);
        animator.SetTrigger(AnimationHash.getHit);
        StartCoroutine(HideHealth(5));
    }

    /// <summary>
    /// 在{time}秒后隐藏血条
    /// </summary>
    /// <param name="time">间隔的时间</param>
    /// <returns></returns>
    public IEnumerator HideHealth(float time)
    {
        yield return new WaitForSeconds(time);
        HealthBarManager.Instance.UnregisterController(this);
    }

}
