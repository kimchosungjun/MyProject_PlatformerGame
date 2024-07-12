using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("������ Part")]    
    [SerializeField, Tooltip("���丮 Json")] TextAsset storyLine;
    private Dictionary<int, Dialogue> dialogueDictionary = new Dictionary<int, Dialogue>();

    public void Awake()
    {
        LoadJsonFile();
    }

    public void LoadJsonFile()
    {
        if (storyLine != null)
        {
            DialogueList dialogueList = JsonUtility.FromJson<DialogueList>(storyLine.text);
            foreach (var dialogue in dialogueList.dialogues)
            {
                if (!dialogueDictionary.ContainsKey(dialogue.storyID))
                {
                    dialogueDictionary.Add(dialogue.storyID, dialogue);
                }
            }
        }
    }

    public Dialogue LoadDialogue(int _ID)
    {
        if (dialogueDictionary.ContainsKey(_ID))
        {
            return dialogueDictionary[_ID];
        }
        return null;
    }
}
