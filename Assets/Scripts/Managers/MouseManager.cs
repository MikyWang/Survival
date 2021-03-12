using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] Texture2D arrow, select, cutTree, build, move, moveClicked, attack;
    public event Action<Vector3> OnEnvironmentClicked;
    public event Action<IDamage> OnTreeClicked;

    RaycastHit hitInfo;
    bool isMoveClicked;
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.SetCursor(arrow, new Vector2(0, 0), CursorMode.Auto);
            return;
        }
        SetCursorTexture();
        MouseControl();
    }

    void SetCursorTexture()
    {
        if (isMoveClicked) return;

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
            var col = hitInfo.collider;
            switch (col.tag)
            {
                case "Player":
                    GameManager.Instance.ToggleSelector(col.GetComponent<ISelected>());
                    break;
            }
        }
        if (Input.GetMouseButton(1) && hitInfo.collider)
        {
            var col = hitInfo.collider;
            switch (col.tag)
            {
                case "Ground":
                    StartCoroutine(SetMoveCursor());
                    OnEnvironmentClicked?.Invoke(hitInfo.point);
                    break;
                case "Tree":
                    var target = col.GetComponent<IDamage>();
                    OnTreeClicked?.Invoke(target);
                    break;
                case "Building":
                    if (col.TryGetComponent<Building>(out var building))
                    {
                        GameManager.Instance.CallBuildersToBuildBuilding(building);
                    }
                    break;
            }
        }
    }

    IEnumerator SetMoveCursor()
    {
        isMoveClicked = true;
        Cursor.SetCursor(moveClicked, new Vector2(0, 0), CursorMode.Auto);
        yield return new WaitForSeconds(0.1f);
        isMoveClicked = false;
    }

}
