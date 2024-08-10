using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHPUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] GameObject hpBarObj;

    BossStat _stat;

    public void Init(BossStat _stat)
    {
        slider.maxValue = _stat.maxHP;
        slider.value = _stat.maxHP;
        hpText.text = slider.value + "/" + slider.maxValue;
    }

    public void UpdateCurHpBar(float _curValue)
    {
        slider.value = Mathf.Round(_curValue * 10f) / 10f;
        float roundedHP = slider.value;
        hpText.text = roundedHP.ToString("0");
        hpText.text += "/" + slider.maxValue;
    }

    public void OnOffUI(bool _isActive)
    {
        hpBarObj.SetActive(_isActive);
    }
}
