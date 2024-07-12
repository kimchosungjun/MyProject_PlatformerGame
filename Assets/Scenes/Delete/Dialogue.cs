using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int storyID; // Json Name
    public string storySpeaker; // Json Name
    public List<string> storyLines; // Json Name
}

[System.Serializable]
public class DialogueList
{
    public List<Dialogue> dialogues; // Json Name
}
