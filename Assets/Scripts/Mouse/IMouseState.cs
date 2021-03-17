using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseState
{
    public string tag { get; }
    public void Hover();
    public void LeftClick(ref RaycastHit hit);
    public void RightClick(ref RaycastHit hit);
}
