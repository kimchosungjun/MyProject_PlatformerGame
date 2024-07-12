using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceNPC : NPC
{
    [SerializeField] string talkID;
    [SerializeField] SpriteOutline outline;
    bool isStartDialogue = true;
    bool isCollidePlayer = false;

    EnhanceUI enhance = null;
    EnhanceUI Enhance { get { if (enhance == null) enhance = GameManager.Instance.UI_Controller.Enhance; return enhance; } }
    public void Talk()
    {
        // use talkid
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
        if (isCollidePlayer && Input.GetKeyDown(KeyCode.F))
        {
            if (!isStartDialogue)
            {
                isStartDialogue = true;
                Talk();
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
            if (GameManager.Instance.UI_Controller == null)
                return;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, this.transform);
        }
    }
}
