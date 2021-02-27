using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class MonsterController : ControllerBase
{
    public float sightRadius;
    private Animator animator;
    private NavMeshAgent agent;
    private MonsterState state;
    private IDamage target;
    private GameObject patrolTarget;
    public Attack attack => skills[SkillId.Attack] as Attack;
    public Hit hit => skills[SkillId.Hit] as Hit;
    private List<GameObject> patrolPoints => GameManager.Instance.patrolPoints;
    public bool isOnPatrolTarget => patrolTarget == null || Vector3.SqrMagnitude(transform.position - patrolTarget.transform.position) <= agent.stoppingDistance;
    public bool HasPlayer
    {
        get
        {
            var colliders = Physics.OverlapSphere(transform.position, sightRadius);
            var col = colliders.FirstOrDefault(col => col.CompareTag("Player"));
            if (col != default(Collider))
            {
                target = col.gameObject.GetComponent<IDamage>();
            }
            return col != default(Collider);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        speed = agent.velocity.sqrMagnitude;
        SwitchState();
    }
    private void SwitchState()
    {
        state = isChasing ? MonsterState.Chase : MonsterState.Patrol;
        switch (state)
        {
            case MonsterState.Patrol:
                OnPatrol();
                break;
            case MonsterState.Chase:
                OnChase();
                break;
        }
    }

    private void OnChase()
    {
        if (!HasPlayer)
        {
            isChasing = false;
            target = null;
            attack.Interrupt();
            hit.Interrupt();
            agent.destination = transform.position;
            return;
        }
        Attack();
    }

    private void OnPatrol()
    {
        if (HasPlayer)
        {
            isChasing = true;
            patrolTarget = null;
            return;
        }
        if (isOnPatrolTarget)
        {
            isWalking = false;
            if (!isThinking)
            {
                animator.SetTrigger(AnimationHash.think);
                return;
            }
            var index = UnityEngine.Random.Range(0, 7);
            patrolTarget = patrolPoints[index];
        }
        if (isThinking) return;
        Move(patrolTarget.transform.position);
    }

    public override void Attack()
    {
        attack.Excute(target);
        hit.Excute(target);
    }

    /// <summary>
    /// 动画事件
    /// </summary>
    private void HitTarget()
    {
        target.TakingDamage(stats.attack);
    }
    public override void Move(Vector3 target)
    {
        agent.destination = target;
        isWalking = true;

    }
    public override void OnDeathAnimEnd()
    {

    }

    public override IEnumerator TakingDizzy(float time)
    {
        yield break;
    }

    public override void TakingHit(int damage, float time)
    {
        throw new System.NotImplementedException();
    }

    public override void TakingDamage(int damage)
    {
        stats.TakingDamage(damage);
    }

    public override void RecoverHP(int point)
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

}
