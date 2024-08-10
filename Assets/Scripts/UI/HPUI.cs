using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI hpText;
    PlayerController controller;

    private void Start()
    {
        if (controller == null)
            controller = GameManager.Instance.Controller;
        slider.maxValue = controller.MaxHp;
        slider.value = controller.CurHP;
        slider.value = Mathf.Round(controller.CurHP * 10f) / 10f;
        float roundedHP = slider.value;
        //hpText.text = roundedHP.ToString("0.0");
        hpText.text = roundedHP.ToString("0");
        hpText.text += "/" + slider.maxValue;
    }

    public void UpdateMaxHPBar(float _maxValue, float _curValue)
    {
        slider.maxValue = _maxValue;
        slider.value = _curValue;
        hpText.text = (int)slider.value + "/" + (int)slider.maxValue;
    }
    

    public void UpdateCurHpBar(float _curValue)
    {
        slider.value = _curValue;
        hpText.text = (int)slider.value + "/" + (int)slider.maxValue;
    }
}
