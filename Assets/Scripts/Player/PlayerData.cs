using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    // Speed
    public float xSpeed;
    public float ySpeed;
    public float rollSpeed;
    // Mana
    public int curMana;
    public int maxMana;
    public int attackDecreaseMana;
    public int buffSkillDecreaseMana;
    public int attackSkillDecreaseMana;
    // Stat
    public float curHP;
    public float maxHP;
    public float defenceValue;
    public float damageValue;
    public float buffSkillValue;
    // Time
    public float rollTime;
    public float rollCoolTime;
    public float attackCoolTime;
    public float buffSkillCoolTime;
    public float attackSkillCoolTime;
    public float buffMaintainTime;
    // Enhance Degree
    public int curHpDegree;
    public int curRollDegree;
    public int curAttackDegree;
    public int curBuffDegree;
}

public class EnhanceValueData
{  
    // Enhance Max Degree
    public int enhanceHpMaxDegree = 3;
    public int enhanceRollMaxDegree = 3;
    public int enhanceAttackMaxDegree = 3;
    public int enhanceBuffMaxDegree = 3;
    // Increase Value
    public float[] increaseHpValue = { 15f, 15f, 20f };
    public float[] increaseRollValue = { 1f, 1f, 2f };
    public float[] increaseAttackValue = { 1f, 1f, 2f };
    public float[] increaseBuffValue = { 0.1f, 0.1f, 0.2f };
    // Cost
    public int[] aumCost = { 3, 4, 5 };
}