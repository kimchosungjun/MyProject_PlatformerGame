using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] int _ID;
    [SerializeField, Range(0.1f, 0.5f)] float typeTime;
    [SerializeField, Range(0.1f,1f)] float nextDialogueTime;
    Conversation conversation = new Conversation();

    [SerializeField] int curIdx = 0;
    bool isStart = false;
    public void LoadDiagloue()
    {
        curIdx = 0;
        conversation = ConversationManager.Instance.LoadDialogue(_ID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isStart)
            StartDialogue();
    }

    public void StartDialogue()
    {
        isStart = true;
        LoadDiagloue();
        StartCoroutine(DialogueCor());
    }

    public IEnumerator DialogueCor()
    {
        int len = conversation.lines[curIdx].Length;
        dialogueText.text = "";
        for (int i = 0; i < len; i++)
        {
            dialogueText.text += conversation.lines[curIdx][i];
            yield return new WaitForSeconds(typeTime);
        }
        curIdx += 1;
        yield return new WaitForSeconds(nextDialogueTime);
        CheckNextDialogue();
    }

    public void CheckNextDialogue()
    {
        int len = conversation.lines.Count;
        if (curIdx >= len)
        {
            // 대화 종료
            isStart = false;
            gameObject.SetActive(false);
        }
        else
        {
            // 다음 대화
            StartCoroutine(DialogueCor());
        }
    }
}
