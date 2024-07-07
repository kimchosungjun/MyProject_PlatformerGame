using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="Player/PlayerData", order = int.MinValue)]
public class PlayerData : ScriptableObject
{
    public PlayerType playerType;

    public float xSpeed;
    public float ySpeed;
    public float rollSpeed;
    public float rollTimer;
    public float rollCoolTime;

    public float curhp;
    public float maxhp;
    public float defenceValue;
    public float attackValue;
    public float buffSkillValue;
    public float attackSkilldamage;



    public void Init()
    {
        switch (playerType)
        {
            case PlayerType.Normal:
                break;
            case PlayerType.Wind:
                break;
            case PlayerType.Water:
                break;
            default:
                break;
        }
    }
}
