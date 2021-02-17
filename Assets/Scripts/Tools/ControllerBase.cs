using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour, IDamage
{
    public abstract float speed { get; set; }
    public abstract bool isHitting { get; set; }
    public abstract bool isDizzying { get; set; }
    public abstract bool isThinking { get; set; }
    public abstract bool useSkill { get; set; }

    public abstract void Move(Vector3 target);
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
}


