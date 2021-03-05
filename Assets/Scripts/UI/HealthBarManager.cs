using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBarManager : Singleton<HealthBarManager>
{
    public GameObject healthBarPrefab;
    public List<ControllerBase> lives { get; private set; } = new List<ControllerBase>();
    public Dictionary<int, GameObject> liveHealthBars { get; private set; } = new Dictionary<int, GameObject>();
    void LateUpdate()
    {
        foreach (var live in lives)
        {
            var hashId = live.gameObject.GetInstanceID();
            if (live.transform.IsInView())
            {
                if (!liveHealthBars.ContainsKey(hashId))
                {
                    var health = Instantiate(healthBarPrefab, transform);
                    liveHealthBars.Add(hashId, health);
                    var bar = health.GetComponent<HealthBar>();
                    bar.UpdateBarUI(live.stats);
                    //TODO:修复订阅错误
                    live.stats.Subscribe(bar);
                }
                liveHealthBars[hashId].SetActive(true);
                var pos = live.headUITransform.position;
                liveHealthBars[hashId].transform.position = pos;
                liveHealthBars[hashId].transform.forward = Camera.main.transform.forward;
            }
            else
            {
                if (liveHealthBars.ContainsKey(hashId))
                {
                    liveHealthBars[hashId].SetActive(false);
                }
            }
        }
    }

    public void RegisterController(ControllerBase controller)
    {
        if (lives.Contains(controller)) return;
        lives.Add(controller);
    }
    public void UnregisterController(ControllerBase controller)
    {
        if (!lives.Contains(controller)) return;
        var hashId = controller.gameObject.GetInstanceID();
        lives.Remove(controller);
        if (liveHealthBars.ContainsKey(hashId))
        {
            liveHealthBars[hashId].SetActive(false);
        }
    }

}
