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
                }
                liveHealthBars[hashId].SetActive(true);
                liveHealthBars[hashId].GetComponent<HealthBar>().UpdateBarUI(live);
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
