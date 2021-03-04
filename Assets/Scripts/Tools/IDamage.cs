using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public GameObject self { get; }
    public LiveStats stats { get; }
    public bool isHitting { get; set; }
    public bool isDizzying { get; set; }
    public bool isDead { get; }
    public void TakingDamage(ControllerBase attacker, int damage);
    public void RecoverHP(int point);
    public void TakingHit(ControllerBase attacker, int damage, float time);
    public IEnumerator TakingDizzy(float time);
}
