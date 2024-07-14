using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TalkData",menuName = "NPC/TalkData")]
public class NPCTalkData : ScriptableObject
{
    public bool isTalk;

    public void Init()
    {
        isTalk = false;
    }
}
