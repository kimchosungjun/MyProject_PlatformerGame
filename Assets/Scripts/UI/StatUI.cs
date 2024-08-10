using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatUI : EscapeUI
{
    [SerializeField] GameObject statObject;
    // Hp, Roll, Attack, Buff
    [SerializeField] TextMeshProUGUI[] statTexts;
    PlayerData playerData;
    public override void TurnOnOffUI(bool _isActive = false)
    {
        if (IsOn)
        {
            statObject.SetActive(false);
            IsOn = false;
        }
    }

   
    public void PressStatBtn()
    {
        if (IsOn)
        {
            statObject.SetActive(false);
        }
        else
        {
            if (playerData == null)
            {
                playerData = GameManager.Instance.Controller.PData;
            }
            statTexts[0].text = $"�ִ� ü�� : {playerData.maxHP}";
            statTexts[1].text = $"������ �� Ÿ�� : {playerData.rollCoolTime}";
            statTexts[2].text = $"���ݷ� : {playerData.damageValue}";
            statTexts[3].text = $"���� ������(���ݷ�) : {playerData.buffSkillValue}";
            statObject.SetActive(true);
        }
        IsOn = !IsOn;
    }

    public void ExitStatBtn()
    {
        IsOn = false;
        statObject.SetActive(false);
    }
}
