using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceNPC : MonoBehaviour
{
    string talkerName = "Erica";
    TalkData talkData =null;
    public TalkData Talk_Data { get { if (talkData == null) talkData = GameManager.Instance.TalkData_Manager.LoadTalk(talkerName); return talkData; } }

    [SerializeField] int talkID;
    [SerializeField] SpriteOutline outline;
    bool isCollidePlayer = false;

    EnhanceUI enhance = null;
    EnhanceUI Enhance { get { if (enhance == null) enhance = GameManager.Instance.UI_Controller.Enhance; return enhance; } }

    bool canTalk = true;

    public void Talk()
    {
        GameManager.Instance.UI_Controller.Dialogue.StartDialogue(talkID);
    }

    private void Start()
    {
        canTalk = !Talk_Data.isTalk;
    }
    public void EnhanceStat()
    {
        if (!Enhance.IsUseBuff)
            Enhance.TurnOnOffUI(true);
        else
            Enhance.ShowBuffWarnMessage();
    }

    private void Update()
    {
        if (!GameManager.Instance.UI_Controller.Dialogue.IsStartDialogue && isCollidePlayer && Input.GetKeyDown(KeyCode.F))
        {
            if (canTalk)
            {
                Talk();
                talkData.isTalk = true;
                canTalk = false;
                return;
            }
            else
            {
                EnhanceStat();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            outline.enabled = true;
            isCollidePlayer = true;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(true, this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            outline.enabled = false;
            isCollidePlayer = false;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, this.transform);
        }
    }
}
