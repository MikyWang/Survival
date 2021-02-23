using UnityEngine;
public partial class PlayerController
{

    public override float speed
    {
        get
        {
            return animator.GetFloat(AnimationHash.speedHash);
        }
        set
        {
            animator.SetFloat(AnimationHash.speedHash, value);
        }
    }

    public override bool isHitting { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isDizzying { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isThinking { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool useSkill { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool isSelected { get; private set; }

    public GameObject selectedObject => gameObject;
}