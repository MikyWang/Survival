using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : Singleton<MouseManager>
{
    public Texture2D arrow, select, cutTree;
    public event Action<Vector3> OnEnvironmentClicked;

    RaycastHit hitInfo;
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        SetCursorTexture();
        MouseControl();
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
                    Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Tree":
                    Cursor.SetCursor(cutTree, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Player":
                    Cursor.SetCursor(select, new Vector2(16, 16), CursorMode.Auto);
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
                case "Ground":
                    OnEnvironmentClicked?.Invoke(hitInfo.point);
                    break;
                case "Player":
                    GameManager.Instance.RegisterSelectors(col.GetComponent<ISelected>());
                    break;
            }

        }
    }

}
