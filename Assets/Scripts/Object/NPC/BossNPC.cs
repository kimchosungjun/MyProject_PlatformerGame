using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNPC : MonoBehaviour
{
    [SerializeField] int talkID;
    bool canTalk = true;

    public void Talk()
    {
        GameManager.Instance.UI_Controller.Dialogue.StartDialogue(talkID);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTalk)
        {
            canTalk = false;
            Talk();
        }
    }
}
