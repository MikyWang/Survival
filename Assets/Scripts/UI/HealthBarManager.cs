using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                var pos = live.headUITransform.position;
                if (!liveHealthBars.ContainsKey(hashId))
                {
                    var health = Instantiate(healthBarPrefab, transform);
                    liveHealthBars.Add(hashId, health);
                }
                liveHealthBars[hashId].transform.position = pos;
                // liveHealthBars[hashId].transform.rotation = Camera.main.transform.rotation;
                liveHealthBars[hashId].transform.forward = Camera.main.transform.forward;
                liveHealthBars[hashId].SetActive(true);
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
        lives.Add(controller);
    }

}
