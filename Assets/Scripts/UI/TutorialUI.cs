using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;


public class TutorialUI : EscapeUI
{
    [SerializeField] GameObject tutorialObject;
    [SerializeField] TextMeshProUGUI tutorialName;
    [SerializeField] TextMeshProUGUI tutorialInfo;

    public override void TurnOnOffUI(bool _isActive)
    {
        tutorialObject.SetActive(_isActive);
        if (_isActive)
            Time.timeScale = 0f;
        else
        {
            GameManager.Instance.Video.Stop();
            Time.timeScale = 1f;
        }
        IsOn = _isActive;
    }

    public void Set(string _name, string _info, VideoClip _clip)
    {
        GameManager.Instance.Video.clip = _clip;
        GameManager.Instance.Video.Play();
        tutorialName.text = _name;
        tutorialInfo.text = _info;
        Invoke("TurnOnDelay", 0.1f);
    }

    public void TurnOnDelay()
    {
        GameManager.Instance.UI_Controller.Tutorial.TurnOnOffUI(true);
    }

    public void ExitBtn()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        GameManager.Instance.Video.Stop();
        tutorialObject.SetActive(false);
        IsOn = false;
        Time.timeScale = 1f;
    }
}
