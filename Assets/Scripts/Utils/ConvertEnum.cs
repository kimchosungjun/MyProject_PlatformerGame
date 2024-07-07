using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertEnum 
{
    public static string  ConvertEnumToString<T>(T _enumType) where T : Enum 
    {
        return Enum.GetName(typeof(T),_enumType);
    }

    public static int ConvertEnumToInt<T>(T _enumType) where T : Enum
    {
        return Convert.ToInt32(_enumType);
    }
}

public enum LobbyUIType
{
    Start,
    Exit
}

public enum PlayerType
{
    Normal,
    Wind,
    Water,
    None
}

public enum PlayerActionType
{
    Idle=0,
    Move,
    Jump,
    Fall,
    Roll,
}

public enum PaltformType
{
    Ground,
    JumpPlatform
}

public enum SkillType
{
    Attack,
    Buff,
    AttackSkill
}