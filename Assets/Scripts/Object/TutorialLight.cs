using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialLight : MonoBehaviour
{
    TalkData talkData;

    [SerializeField] string talkName;
    [SerializeField, TextArea] string nameStr;
    [SerializeField, TextArea] string infoStr;
    [SerializeField] VideoClip videoClip;

    private void Start()
    {
        if (talkData == null)
            talkData = GameManager.Instance.TalkData_Manager.LoadTalk(talkName);

        if (talkData.isTalk)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.UI_Controller.Tutorial.Set(nameStr, infoStr, videoClip);
            GameManager.Instance.UI_Controller.Tutorial.TurnOnOffUI(true);
            talkData.isTalk = true;
            Destroy(gameObject);
        }
    }
}
