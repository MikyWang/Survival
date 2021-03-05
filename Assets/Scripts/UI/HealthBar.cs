using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HealthBar : MonoBehaviour, IObserver<LiveStats>
{
    public Image healthSlider;
    public Image expSlider;
    public TMP_Text NameText;
    public TMP_Text healthText;
    public TMP_Text levelText;
    public TMP_Text expText;
    private GameObject levelImg;
    private GameObject levelHolder;
    public void Awake()
    {
        levelImg = levelText.transform.parent.gameObject;
        levelHolder = expText.transform.parent.gameObject;
        levelImg.SetActive(false);
        levelHolder.SetActive(false);
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(LiveStats value)
    {
        UpdateBarUI(value);
    }

    public void UpdateBarUI(LiveStats stats)
    {
        if (stats == null) return;

        healthText.text = $"{stats.health}/{stats.maxHealth}";
        NameText.text = stats.liveName;
        if (stats.CompareTag("Player"))
        {
            NameText.color = new Color(0, 176, 249, 255);
        }
        else
        {
            NameText.color = Color.red;
        }
        if (stats.maxLevelPoint > 0)
        {
            levelText.text = stats.level.ToString();
            expSlider.fillAmount = (float)stats.levelPoint / stats.maxLevelPoint;
            expText.text = $"{stats.levelPoint}/{stats.maxLevelPoint}";
            levelImg.SetActive(true);
            levelHolder.SetActive(true);
        }
        healthSlider.fillAmount = (float)stats.health / stats.maxHealth;
    }

}
