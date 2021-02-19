using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class PlayerController : ControllerBase, ISelected
{
    private NavMeshAgent agent;
    private Animator animator;
    private LiveStats stats;

    private void Awake()
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
        GameManager.Instance.ToggleSelectors(this);
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

    public void Select(GameObject highlightingPrefab)
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

    public void UnSelect()
    {
        var ring = transform.Find("Highlight Ring(Clone)");
        ring?.gameObject.SetActive(false);
        MouseManager.Instance.OnEnvironmentClicked -= Move;
    }
}
