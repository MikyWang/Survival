using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    RaycastHit hitInfo;
    IMouseState currentState;
    Dictionary<Tag, IMouseState> mouseStates;
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        InitCursorData();
    }
    private void Update()
    {
        // SetCursorTexture();
        SwitchMouseState();
        MouseControl();
    }

    //TODO:添加剩余鼠标状态
    void InitCursorData()
    {
        mouseStates = new Dictionary<Tag, IMouseState>()
        {
            {Tag.Untagged,new MouseDefaultState()},
            {Tag.Ground,new MouseOnGroundState()},
            {Tag.Tree,new MouseOnTreeState()},
            {Tag.Player,new MouseOnPlayerState()},
        };
    }
    void SwitchMouseState()
    {
        hitInfo = Utility.CameraRay();
        var tag = (Tag)Enum.Parse(typeof(Tag), hitInfo.collider?.tag);

        if (EventSystem.current.IsPointerOverGameObject() || !mouseStates.ContainsKey(tag))
        {
            currentState = mouseStates[Tag.Untagged];
        }
        else
        {
            currentState = mouseStates[tag];
        }
        currentState.Hover();
    }

    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider)
        {
            currentState.LeftClick(ref hitInfo);
        }
        if (Input.GetMouseButton(1) && hitInfo.collider)
        {
            currentState.RightClick(ref hitInfo);
        }
    }
}
