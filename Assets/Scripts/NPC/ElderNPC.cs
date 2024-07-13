using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderNPC : MonoBehaviour
{
    [SerializeField] NPCTalkData talkData;
    [SerializeField] int talkID;
    [SerializeField] SpriteOutline outline;
    bool isCollidePlayer = false;
    bool canTalk = true;

    public void Talk()
    {
        GameManager.Instance.UI_Controller.Dialogue.StartDialogue(talkID);
    }

    private void Awake()
    {
        if (talkData.isTalk)
        {
            canTalk = false;
        }
        else
        {
            canTalk = true;
        }
    }

    private void Update()
    {
        if (canTalk)
        {
            if (!GameManager.Instance.UI_Controller.Dialogue.IsStartDialogue && isCollidePlayer && Input.GetKeyDown(KeyCode.F))
            {
                Talk();
                canTalk = false;
                talkData.isTalk = true;
                outline.enabled = false;
                isCollidePlayer = false;
                GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, this.transform);
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTalk)
        {
            outline.enabled = true;
            isCollidePlayer = true;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(true, this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTalk)
        {
            outline.enabled = false;
            isCollidePlayer = false;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, this.transform);
        }
    }
}
