using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="Player/PlayerData", order = int.MinValue)]
public class PlayerData : ScriptableObject
{
    public string attackFabName;
    public string attackSkillFabName;

    public float xSpeed;
    public float ySpeed;
    public float rollSpeed;
    public float attackSpeed;
    public float attackSkillSpeed;

    public float rollTimer; // 구르는 시간
    public float rollCoolTime; // 구르고 난 뒤에 다시 구르기까지 걸리는 시간
    public float attackCoolTime;
    public float buffSkillCoolTime;
    public float attackSkillCoolTime;
    public float buffMaintainTime; // 버프 지속시간

    public float maxHp;
    public int curMana;
    public int maxMana;

    public float defenceValue;
    public float attackDamageValue;
    public float buffSkillValue;

    public int attackDecreaseMana;
    public int buffSkillDecreaseMana;
    public int attackSkillDecreaseMana;
}
