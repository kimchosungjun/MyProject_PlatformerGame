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

#region UI
public enum LobbyUIType
{
    Start,
    Continue,
    Exit
}

public enum EnhanceType
{
    HP,
    Roll,
    Attack,
    Buff,
    None
}

#endregion

#region Player
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

public enum SkillType
{
    Attack,
    Buff,
    AttackSkill
}

public enum PressKeyType
{
    Roll,
    Attack,
    Buff,
    AttackSkill
}
#endregion

#region Environment(Tag)
public enum PaltformType
{
    Ground,
    JumpPlatform
}
#endregion

#region Enemy
public enum EnemyActionType
{
    Idle,
    Move,
    Find
}
#endregion

#region Scene
public enum SceneType
{
    Lobby,
    Tutorial,
    Battle,
    Town,
    Boss
}
#endregion

#region Sound
public enum MixerType
{
    Master,
    BGM,
    SFX
}

public enum UISoundType
{
    Warn,
    Click
}

public enum PlayerSoundType
{
    Attack,
    Buff,
    AttackSkill,
    Hit,
    Death
}

public enum EnemySoundType
{
    Hit,
    Death
}
#endregion