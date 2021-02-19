using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RectSelector : MonoBehaviour
{
    private RectTransform boxTransform;
    private Vector2 startPos;
    private bool isSelected = false;
    private Rect rect;

    private void Awake()
    {
        boxTransform = GetComponent<RectTransform>();
    }

    private void StartSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            boxTransform.anchoredPosition = startPos;
            isSelected = true;
        }
    }

    private void OnSelect()
    {
        if (!isSelected)
        {
            boxTransform.sizeDelta = new Vector2(0, 0);
            return;
        }

        var mousePos = Input.mousePosition;
        if (mousePos.x - startPos.x > 0)
        {
            if (startPos.y - mousePos.y > 0)
            {
                boxTransform.pivot = Vector2.up;
                boxTransform.sizeDelta = new Vector2(-(startPos.x - mousePos.x), startPos.y - mousePos.y);
                rect = new Rect(startPos.x, startPos.y - boxTransform.sizeDelta.y, boxTransform.sizeDelta.x, boxTransform.sizeDelta.y);
            }
            else
            {
                boxTransform.pivot = Vector2.zero;
                boxTransform.sizeDelta = new Vector2(-(startPos.x - mousePos.x), -(startPos.y - mousePos.y));
                rect = new Rect(startPos.x, startPos.y, boxTransform.sizeDelta.x, boxTransform.sizeDelta.y);
            }
        }
        else
        {
            if (startPos.y - mousePos.y > 0)
            {
                boxTransform.pivot = Vector2.one;
                boxTransform.sizeDelta = new Vector2(startPos.x - mousePos.x, startPos.y - mousePos.y);
                rect = new Rect(startPos.x - boxTransform.sizeDelta.x, startPos.y - boxTransform.sizeDelta.y, boxTransform.sizeDelta.x, boxTransform.sizeDelta.y);
            }
            else
            {
                boxTransform.pivot = Vector2.right;
                boxTransform.sizeDelta = new Vector2(startPos.x - mousePos.x, -(startPos.y - mousePos.y));
                rect = new Rect(startPos.x - boxTransform.sizeDelta.x, startPos.y, boxTransform.sizeDelta.x, boxTransform.sizeDelta.y);
            }
        }


    }

    private void LateUpdate()
    {

        StartSelect();
        OnSelect();
        EndSelect();

    }

    public void EndSelect()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;

            if (boxTransform.rect.width < 5 || boxTransform.rect.height < 5) return;

            GameManager.Instance.SelectRangePlayers(rect);
        }

    }

}
