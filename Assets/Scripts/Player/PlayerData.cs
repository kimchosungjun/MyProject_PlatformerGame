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

    public float rollTimer; // ������ �ð�
    public float rollCoolTime; // ������ �� �ڿ� �ٽ� ��������� �ɸ��� �ð�
    public float attackCoolTime;
    public float buffSkillCoolTime;
    public float attackSkillCoolTime;
    public float buffMaintainTime; // ���� ���ӽð�

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
