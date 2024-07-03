using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager Instance;
    [SerializeField] TextAsset conversationTextAsset;

    private Dictionary<int, Conversation> conversationDictionary = new Dictionary<int, Conversation>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        LoadJsonFile();
    }

    public void LoadJsonFile()
    {
        if (conversationTextAsset != null)
        {
            ConversationList conversationList = JsonUtility.FromJson<ConversationList>(conversationTextAsset.text);
            foreach (var conversation in conversationList.conversations)
            {
                if (!conversationDictionary.ContainsKey(conversation.ID))
                {
                    conversationDictionary.Add(conversation.ID, conversation);
                }
            }
        }
        else
        {
            Debug.LogError("Conversation text asset is null.");
        }
    }

    public Conversation LoadDialogue(int _ID)
    {
        if (conversationDictionary.ContainsKey(_ID))
        {
            return conversationDictionary[_ID];
        }
        return null;
    }
}
