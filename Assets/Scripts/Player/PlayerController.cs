using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class PlayerController : ControllerBase
{
    private NavMeshAgent agent;
    private Animator animator;
    private LiveStats stats;
    public override float speed
    {
        get
        {
            return animator.GetFloat(_speedHash);
        }
        set
        {
            animator.SetFloat(_speedHash, value);
        }
    }

    public override bool isHitting { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isDizzying { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isThinking { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool useSkill { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<LiveStats>();
    }

    private void Update()
    {
        speed = agent.velocity.sqrMagnitude;
    }
    private void Start()
    {
        GameManager.Instance.RegisterController(this);
        MouseManager.Instance.OnEnvironmentClicked += Move;
    }

    private void OnDestroy()
    {
        if (MouseManager.IsInitialized)
        {
            MouseManager.Instance.OnEnvironmentClicked -= Move;
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move(Vector3 target)
    {
        agent.destination = target;
    }

    public override void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
        if (stats.liveData.health <= 0)
        {
            Debug.Log("角色死亡");
            ///TODO:角色死亡处理
            // isDead = true;
        }
    }
}
