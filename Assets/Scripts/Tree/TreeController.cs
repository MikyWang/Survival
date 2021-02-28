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
    public GameObject stumpPrefab;
    public GameObject smokedPrefab;
    override protected void Awake()
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

    public override void TakingDamage(ControllerBase attacker, int damage)
    {
        stats.TakingDamage(damage);
        animator.SetInteger(AnimationHash.health, stats.health);
        if (stats.isDead)
        {
            (attacker as Peasant)?.AddWood();
            attacker.stats.UpdateLevelExp(stats.deadPoint);
            StartCoroutine(Death());
        }
    }

    public override IEnumerator TakingDizzy(float time)
    {
        yield break;
    }

    public override void TakingHit(ControllerBase attacker, int damage, float time)
    {
        StopAllCoroutines();
        HealthBarManager.Instance.RegisterController(this);
        animator.SetTrigger(AnimationHash.getHit);
        TakingDamage(attacker, damage);
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
    IEnumerator Death()
    {
        Instantiate(smokedPrefab, transform.position, Quaternion.identity);
        Instantiate(stumpPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        HealthBarManager.Instance.UnregisterController(this);
        Destroy(gameObject);
    }
}
