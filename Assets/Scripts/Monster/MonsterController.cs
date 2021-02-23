using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState { Patrol, Chase }

[RequireComponent(typeof(NavMeshAgent))]
public partial class MonsterController : ControllerBase
{
    public float sightRadius;
    private Animator animator;
    private NavMeshAgent agent;
    private LiveStats stats;
    private MonsterState state;
    private GameObject target;
    private List<GameObject> patrolPoints => GameManager.Instance.patrolPoints;
    //TODO:将攻击改为使用技能方式.
    public bool isOnTarget => target == null || Vector3.SqrMagnitude(target.transform.position - transform.position) <= (useSkill ? stats.skillRange : stats.attackRange);
    public bool HasPlayer
    {
        get
        {
            var colliders = Physics.OverlapSphere(transform.position, sightRadius);
            var col = colliders.FirstOrDefault(col => col.CompareTag("Player"));
            if (col != default(Collider))
            {
                target = col.gameObject;
            }
            return col != default(Collider);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<LiveStats>();
    }
    private void Update()
    {
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
            agent.destination = transform.position;
            return;
        }
        if (isOnTarget)
        {
            isWalking = false;
            agent.destination = transform.position;
            Attack();
        }
        else
        {
            Move(target.transform.position);
        }
    }

    private void OnPatrol()
    {
        if (HasPlayer)
        {
            isChasing = true;
            return;
        }
        if (isOnTarget)
        {
            isWalking = false;
            if (!isThinking)
            {
                animator.SetTrigger(AnimationHash.think);
                return;
            }
            var index = UnityEngine.Random.Range(0, 7);
            target = patrolPoints[index];
        }
        if (isThinking) return;
        Move(target.transform.position);
    }

    public override void Attack()
    {
        if (stats.CheckSkill())
        {
            useSkill = true;
            animator.SetTrigger(AnimationHash.attack);
            return;
        }
        if (stats.CheckAttack())
        {
            animator.SetTrigger(AnimationHash.attack);
            return;
        }

    }

    /// <summary>
    /// 动画事件
    /// </summary>
    private void HitTarget()
    {
        target?.GetComponent<IDamage>().TakingDamage(stats.attack);
    }
    public override void Move(Vector3 target)
    {
        agent.destination = target;
        isWalking = true;

    }

    public override void TakingDamage(int damage)
    {
        stats.TakingDamage(damage);
    }

    public override void TakingDizzy()
    {
        throw new System.NotImplementedException();
    }

    public override void TakingHit()
    {
        throw new System.NotImplementedException();
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
