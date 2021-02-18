using UnityEngine;
public partial class PlayerController
{
    private readonly int _speedHash = Animator.StringToHash("speed");

    public override float speed
    {
        get
        {
            return animator.GetFloat(_speedHash);
        }
        set
        {
            animator.SetFloat(_speedHash, value);
        }
    }

    public override bool isHitting { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isDizzying { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isThinking { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool useSkill { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool isSelected { get; private set; }

    public GameObject selectedObject => gameObject;
}