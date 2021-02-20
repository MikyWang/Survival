using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class PlayerController : ControllerBase, ISelected
{
    protected NavMeshAgent agent;
    protected Animator animator;
    protected LiveStats stats;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<LiveStats>();
    }

    private void Update()
    {
        speed = agent.velocity.sqrMagnitude;
        GameManager.Instance.UpdatePlayersInView(this);
    }
    private void Start()
    {
        GameManager.Instance.ToggleSelector(this);
    }

    private void OnDestroy()
    {
        if (MouseManager.IsInitialized && isSelected)
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

    public override void TakingDamage(int damage)
    {
        stats.TakingDamage(damage);
        if (stats.liveData.health <= 0)
        {
            Debug.Log("角色死亡");
            ///TODO:角色死亡处理
            // isDead = true;
        }
    }

    public virtual void Select(GameObject highlightingPrefab)
    {
        var ring = transform.Find("Highlight Ring(Clone)");
        if (ring != null)
        {
            ring.gameObject.SetActive(true);
        }
        else
        {
            Instantiate(highlightingPrefab, transform);
        }
        MouseManager.Instance.OnEnvironmentClicked += Move;

    }

    public virtual void UnSelect()
    {
        var ring = transform.Find("Highlight Ring(Clone)");
        ring?.gameObject.SetActive(false);
        MouseManager.Instance.OnEnvironmentClicked -= Move;
    }

    public override void TakingHit()
    {
        throw new System.NotImplementedException();
    }

    public override void TakingDizzy()
    {
        throw new System.NotImplementedException();
    }

    public override void RecoverHP(int point)
    {
        throw new System.NotImplementedException();
    }
}
