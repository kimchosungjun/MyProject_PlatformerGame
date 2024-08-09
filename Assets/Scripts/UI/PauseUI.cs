using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseUI : EscapeUI
{
    [SerializeField] GameObject pauseObject;
    [SerializeField] GameObject decideObject;
    [SerializeField] Button[] btns;

    public override void TurnOnOffUI(bool _isActive)
    {
        pauseObject.SetActive(_isActive);
        if (_isActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        TurnOnOffUI(false);
    }

    public void ReturnLobby()
    {
        decideObject.SetActive(true);
        int cnt = btns.Length;
        for(int i=0; i<cnt; i++)
        {
            btns[i].interactable = false;
        }
    }

    public void PressSoundManage()
    {
        GameManager.Instance.UI_Controller.Sound.TurnOnOffUI(true);
    }

    public void DecideLobby()
    {
        GameManager.Instance.UI_Controller.Fade.LoobyFadeOut();
        GameManager.Instance.SaveAllData();
    }

    public void CancleDecideLobby()
    {
        decideObject.SetActive(false);
        int cnt = btns.Length;
        for (int i = 0; i < cnt; i++)
        {
            btns[i].interactable = true;
        }
    }
}
