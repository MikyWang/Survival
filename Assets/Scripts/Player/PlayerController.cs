using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class PlayerController : ControllerBase, ISelected
{
    protected NavMeshAgent agent;
    protected Animator animator;
    override protected void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        speed = agent.velocity.sqrMagnitude;
        GameManager.Instance.UpdatePlayersInView(this);
    }
    protected override void Start()
    {
        base.Start();
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

    public override void TakingDamage(ControllerBase attacker, int damage)
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
            ring.DOScale(0, .2f).From().SetEase(Ease.OutBack);
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



    public override void RecoverHP(int point)
    {
        throw new System.NotImplementedException();
    }

    public override void TakingHit(ControllerBase attacker, int damage, float time)
    {
        TakingDamage(attacker, damage);
        StartCoroutine(TakingDizzy(time));
    }

    public override IEnumerator TakingDizzy(float time)
    {
        while (time > 0)
        {
            isDizzying = true;
            time -= Time.deltaTime;
            yield return null;
        }
        isDizzying = false;
    }

}
