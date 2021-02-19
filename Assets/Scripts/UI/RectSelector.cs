using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RectSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image rectImg;

    private Vector2 startPos;
    private Vector2 endPos;
    private bool isSelected = false;
    private RectTransform rectTransform;
    private RectTransform panelTransform;
    private PointerEventData currentPE;

    private void Start()
    {
        rectTransform = rectImg.GetComponent<RectTransform>();
        panelTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
        rectTransform.position = startPos;
        isSelected = true;
        currentPE = eventData;
    }

    private void LateUpdate()
    {
        if (!isSelected) return;

        endPos = currentPE.position;
        Debug.Log($"startPos{startPos}---endPos{endPos}");
        var width = Mathf.Abs(endPos.x - startPos.x);
        var height = Mathf.Abs(endPos.y - startPos.y);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        rectImg.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
        rectImg.gameObject.SetActive(false);
    }

}
