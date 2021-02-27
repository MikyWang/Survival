using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public GameObject self { get; }
    public bool isHitting { get; set; }
    public bool isDizzying { get; set; }
    public bool isDead { get; }
    public void TakingDamage(int damage);
    public void RecoverHP(int point);
    public void TakingHit(int damage, float time);
    public IEnumerator TakingDizzy(float time);
}
