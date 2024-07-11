using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="Player/PlayerData", order = int.MinValue)]
public class PlayerData : ScriptableObject
{
    [Header("Player Do not change Info")]
    public PlayerType currentType;
    public string attackFabName;
    public string attackSkillFabName;
    public int attackDecreaseMana;
    public int buffSkillDecreaseMana;
    public int attackSkillDecreaseMana;

    [Header("Player Speed")]
    public float xSpeed;
    public float ySpeed;
    public float rollSpeed;
    
    [Header("Player change Info")]
    public int curMana;
    public int maxMana;
    public float defenceValue;

    [Header("Player Common Info")]
    public float maxHp;
    public float attackDamageValue;
    public float buffSkillValue = 1.1f;

    [Header("Timer")]
    public float rollTimer; // 구르는 시간
    public float rollCoolTime =8; // 구르고 난 뒤에 다시 구르기까지 걸리는 시간
    public float attackCoolTime;
    public float buffSkillCoolTime;
    public float attackSkillCoolTime;
    public float buffMaintainTime; // 버프 지속시간

    [Header("EnhanceDegree")]

    public int curHpDegree;
    public int curRollDegree;
    public int curAttackDegree;
    public int curBuffDegree;

    public void Init()
    {
        rollCoolTime = 8f;
        maxHp = 100f;
        maxMana = 100;
        buffSkillValue = 1.1f;

        curHpDegree=0;
        curRollDegree=0;
        curAttackDegree=0;
        curBuffDegree=0;

        switch (currentType)
        {
            case PlayerType.Normal:
                attackDamageValue = 5f;
                defenceValue = 1f;
                xSpeed = 6f;
                ySpeed = 10f;
                break;
            case PlayerType.Wind:
                attackDamageValue = 6.5f;
                defenceValue = 0.5f;
                xSpeed = 8f;
                ySpeed = 16f;
                break;
            case PlayerType.Water:
                attackDamageValue = 3.5f;
                defenceValue = 1.5f;
                xSpeed = 5f;
                ySpeed = 10f;
                break;
        }
        Continue();
    }

    public void Continue()
    {
        if (currentType == PlayerType.Normal)
        {
            curMana = 1;
        }
        else
        {
            curMana = 0;
        }
    }
}
