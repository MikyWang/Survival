using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamage
{
    public abstract float speed { get; set; }
    public abstract bool isHitting { get; set; }
    public abstract bool isDizzying { get; set; }
    public abstract bool isThinking { get; set; }
    public GameObject self => gameObject;
    public abstract void Move(Vector3 target);
    public abstract void Attack();
    public abstract void TakingDamage(int damage);
    public abstract void TakingHit(int damage, float time);
    public abstract IEnumerator TakingDizzy(float time);
    public abstract void RecoverHP(int point);
}


