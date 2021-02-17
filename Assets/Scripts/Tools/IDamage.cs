using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public bool isHitting { get; set; }
    public bool isDizzying { get; set; }
    public void TakeDamage(int damage);
}
