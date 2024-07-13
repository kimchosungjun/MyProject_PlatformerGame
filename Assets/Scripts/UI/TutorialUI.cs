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
            Time.timeScale = 1f;
    }

    public void Set(string _name, string _info, VideoClip _clip)
    {
        GameManager.Instance.Video.clip = _clip;
        tutorialName.text = _name;
        tutorialInfo.text = _info;
    }

    public void ExitBtn()
    {
        tutorialObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
