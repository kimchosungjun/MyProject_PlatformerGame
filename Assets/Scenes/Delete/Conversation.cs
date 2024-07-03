using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conversation
{
    public int ID;
    public string time;
    public List<string> lines;
}

[System.Serializable]
public class ConversationList
{
    public List<Conversation> conversations;
}
