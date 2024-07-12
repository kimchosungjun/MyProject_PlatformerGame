using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("��ȹ�� Part")]
    [SerializeField, Tooltip("���� ��ȭ�� �ڵ����� �ѱ���� �����ϴ� ��")] bool isAutoDialouge;
    [SerializeField, Range(0.1f, 0.5f), Tooltip("Ÿ���� �ӵ�")] float typeTime;
    [SerializeField, Range(0.1f, 1f), Tooltip("��ȭ�� �ڵ����� �Ѿ�� �ӵ�")] float nextDialogueTime;

    [Header("������ Part")]
    [SerializeField] GameObject onoffUI;
    [SerializeField] GameObject skipTextObject;
    [SerializeField] TextMeshProUGUI speakerText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject characterImageObject;
    [SerializeField] Image characterImage;
    [SerializeField] char eventChar ='$';

    Dialogue dialogue = new Dialogue();
    int curIdx = 0; // Current Dialogue Index
    bool isStartDialogue = false; // Prevent Overlap Dialogue
    public bool IsStartDialogue { get { return isStartDialogue; } set {  isStartDialogue = value; GameManager.Instance.Controller.CanControlPlayer = !isStartDialogue; } }
    bool isDoneDialogue = false; // Show Next Dialouge Key

    public void Execute()
    {
        if(isDoneDialogue && Input.anyKeyDown)
        {
            CheckNextDialogue();
        }
    }

    public void StartDialogue(int _storyID)
    {
        if (isStartDialogue)
            return;
        if (dialogue == null)
            return;
        onoffUI.SetActive(true);
        IsStartDialogue = true;
        LoadDiagloue(_storyID);
        StartCoroutine(DialogueCor());
    }

    public void LoadDiagloue(int _storyID)
    {
        curIdx = 0;
        dialogue = GameManager.Instance.Dialogue_Manager.LoadDialogue(_storyID);
        if (dialogue == null)
            return;
        speakerText.text = dialogue.storySpeaker;
    }

    public IEnumerator DialogueCor()
    {
        isDoneDialogue = false;
        int len = dialogue.storyLines[curIdx].Length;
        dialogueText.text = "";

        // �̺�Ʈ üũ
        char startChar = dialogue.storyLines[curIdx][0];
        if (startChar == eventChar)
        {
            SetDialougeEvent(dialogue.storyLines[curIdx]);
            curIdx += 1;
            CheckNextDialogue();
            yield break;
        }

        for (int i = 0; i < len; i++)
        {
            dialogueText.text += dialogue.storyLines[curIdx][i];
            yield return new WaitForSeconds(typeTime);
        }
        curIdx += 1;
        if (isAutoDialouge)
        {
            yield return new WaitForSeconds(nextDialogueTime);
            CheckNextDialogue();
        }
        else
        {
            isDoneDialogue = true;
            skipTextObject.SetActive(true);
        }
    }

    public void CheckNextDialogue()
    {
        int len = dialogue.storyLines.Count;
        skipTextObject.SetActive(false);
        if (curIdx >= len)
        {
            // ��ȭ ����
            IsStartDialogue = false;
            isDoneDialogue = false;
            onoffUI.SetActive(false);
            characterImageObject.SetActive(false);
        }
        else
        {
            // ���� ��ȭ
            StartCoroutine(DialogueCor());
        }
    }

    #region Dialouge Event System
    public void SetDialougeEvent(string _dialogue)
    {
        int len = _dialogue.Length;
        string eventName = "";
        string parameter="";
        bool isEndEventName = false;
        bool isParameterStart = false;
        List<string> parameterList = new List<string>();
        for(int i=1; i<len; i++)
        {
            if (_dialogue[i] == '(')
            {
                isEndEventName = true;
                isParameterStart = true;
            }
            else if (_dialogue[i] == ')')
            {
                parameterList.Add(parameter);
                isParameterStart = false;
                break;
            }
            else if (_dialogue[i] == ',')
            {
                parameterList.Add(parameter);
                parameter = "";
            }
            if (isParameterStart && _dialogue[i] != '(')
                parameter += _dialogue[i];

            if(!isEndEventName)
                eventName += _dialogue[i];
        }
        DialogueEvent(eventName, parameterList);
    }

    public void DialogueEvent(string _eventName, List<string> _eventParameters)
    {

        switch (_eventName)
        {
            case "Speaker":
                SetSpeakerName(_eventParameters);
                break;
            case "Character":
                ShowCharacterImage(_eventParameters);
                break;
        }
    }
    #endregion

    #region Dialogue Event Method
    public void SetSpeakerName(List<string> _eventParameters)
    {
        speakerText.text = _eventParameters[0];
    }

    public void ShowCharacterImage(List<string> _eventParameters)
    {
        Sprite imageSpr = Resources.Load<Sprite>($"Image/{_eventParameters[0]}");
        if (imageSpr == null)
        {
            Debug.LogError("�̹����� �����ϴ�!!");
            return;
        }
        characterImage.sprite = imageSpr;
        characterImageObject.SetActive(true);
    }
    #endregion
}
