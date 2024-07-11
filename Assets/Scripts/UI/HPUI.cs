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
        hpText.text = slider.value + "/" + slider.maxValue;
    }

    public void UpdateMaxHPBar(float _maxValue, float _curValue)
    {
        slider.maxValue = _maxValue;
        slider.value = _curValue;
        hpText.text = slider.value + "/" + slider.maxValue;
    }
    

    public void UpdateCurHpBar(float _curValue)
    {
        slider.value = _curValue;
        hpText.text = slider.value + "/" + slider.maxValue;
    }
}
