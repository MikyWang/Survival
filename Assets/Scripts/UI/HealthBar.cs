using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
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

    public void UpdateBarUI(ControllerBase live)
    {
        if (live.stats == null) return;
        var pos = live.headUITransform.position;
        transform.position = pos;
        transform.forward = Camera.main.transform.forward;
        healthText.text = $"{live.stats.health}/{live.stats.maxHealth}";
        NameText.text = live.stats.liveName;
        if (live.CompareTag("Player"))
        {
            NameText.color = new Color(0, 176, 249, 255);
        }
        else
        {
            NameText.color = Color.red;
        }
        if (live.stats.maxLevelPoint > 0)
        {
            levelText.text = live.stats.level.ToString();
            expSlider.fillAmount = (float)live.stats.levelPoint / live.stats.maxLevelPoint;
            expText.text = $"{live.stats.levelPoint}/{live.stats.maxLevelPoint}";
            levelImg.SetActive(true);
            levelHolder.SetActive(true);
        }
        healthSlider.fillAmount = (float)live.stats.health / live.stats.maxHealth;
    }

}
