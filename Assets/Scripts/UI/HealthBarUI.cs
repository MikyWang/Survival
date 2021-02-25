using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:制作HealthBar
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var pos = GameManager.Instance.selectedPlayers[0].selectedObject.transform.position;
        transform.position = Camera.main.WorldToScreenPoint(pos);
    }
}
