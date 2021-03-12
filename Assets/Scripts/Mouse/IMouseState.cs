using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseState
{
    public string tag { get; }
    public void OnStateHover();
    public void OnStateLeftClick();
    public void OnStateRightClick();
}
