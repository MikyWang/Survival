using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] Texture2D arrow, select, cutTree, build, move, moveClicked, attack;
    // public event Action<Vector3> OnEnvironmentClicked;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.SetCursor(arrow, new Vector2(0, 0), CursorMode.Auto);
            return;
        }
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
            {Tag.Tree,new MouseOnTreeState()}
        };
    }
    void SwitchMouseState()
    {
        hitInfo = Utility.CameraRay();
        var tag = (Tag)Enum.Parse(typeof(Tag), hitInfo.collider?.tag);

        if (!mouseStates.ContainsKey(tag))
        {
            currentState = mouseStates[Tag.Untagged];
        }
        else
        {
            currentState = mouseStates[tag];
        }
        currentState.Hover();
    }

    void SetCursorTexture()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            //切换鼠标贴图
            switch (hitInfo.collider.tag)
            {
                case "Ground":
                    Cursor.SetCursor(move, new Vector2(0, 0), CursorMode.Auto);
                    break;
                case "Tree":
                    Cursor.SetCursor(cutTree, new Vector2(0, 0), CursorMode.Auto);
                    break;
                case "Player":
                    Cursor.SetCursor(select, new Vector2(0, 0), CursorMode.Auto);
                    break;
                case "Building":
                    Cursor.SetCursor(build, new Vector2(0, 0), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(arrow, new Vector2(0, 0), CursorMode.Auto);
                    break;
            }
        }

    }
    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider)
        {
            currentState.LeftClick(ref hitInfo);
            // var col = hitInfo.collider;
            // switch (col.tag)
            // {
            //     case "Player":
            //         GameManager.Instance.ToggleSelector(col.GetComponent<ISelected>());
            //         break;
            // }
        }
        if (Input.GetMouseButton(1) && hitInfo.collider)
        {
            currentState.RightClick(ref hitInfo);
            // var col = hitInfo.collider;
            // switch (col.tag)
            // {
            //     case "Ground":
            //         StartCoroutine(SetMoveCursor());
            //         // OnEnvironmentClicked?.Invoke(hitInfo.point);
            //         break;
            //     case "Tree":
            //         var target = col.GetComponent<IDamage>();
            //         OnTreeClicked?.Invoke(target);
            //         break;
            //     case "Building":
            //         if (col.TryGetComponent<Building>(out var building))
            //         {
            //             GameManager.Instance.CallBuildersToBuildBuilding(building);
            //         }
            //         break;
            // }
        }
    }



}
