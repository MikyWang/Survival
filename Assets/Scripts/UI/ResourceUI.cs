using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour, IObserver<Resource>
{
    public ResourceId id;
    TMP_Text text;
    private void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        var resource = GameManager.Instance.GetResource(id);
        resource.Subscribe(this);
    }
    public void OnCompleted() { }
    public void OnError(Exception error) { }
    public void OnNext(Resource resource)
    {
        if (id == ResourceId.Food)
        {
            text.text = $"{resource.amount}/{resource.maxAmount}";
            return;
        }

        text.text = resource.amount.ToString("N0");
    }
}
