using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public GameObject defender { get; }
    public bool isHitting { get; set; }
    public bool isDizzying { get; set; }
    public void TakingDamage(int damage);
    public void RecoverHP(int point);
    public void TakingHit();
    public void TakingDizzy();
}
