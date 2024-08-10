using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseUI : EscapeUI
{
    [SerializeField] GameObject pauseObject;
    [SerializeField] GameObject decideObject;
    [SerializeField] Button[] btns;

    bool isPause = false;
    public override void TurnOnOffUI(bool _isActive = false)
    {
        if (!isPause)
        {
            pauseObject.SetActive(true);
            isPause = true;
            Time.timeScale = 0f;
            GameManager.Instance.Controller.CanControlPlayer = false;
        }
        else
        {
            if (decideObject.activeSelf)
            {
                decideObject.SetActive(false);
                int cnt = btns.Length;
                for (int i = 0; i < cnt; i++)
                {
                    btns[i].interactable = true;
                }
                return;
            }
            isPause = false;
            pauseObject.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.Controller.CanControlPlayer = true;
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        TurnOnOffUI(false);
    }

    public void ReturnLobby()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        decideObject.SetActive(true);
        int cnt = btns.Length;
        for(int i=0; i<cnt; i++)
        {
            btns[i].interactable = false;
        }
    }

    public void PressSoundManage()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        GameManager.Instance.UI_Controller.Sound.TurnOnOffUI(true);
    }

    public void DecideLobby()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        decideObject.SetActive(false);
        pauseObject.SetActive(false);
        GameManager.Instance.UI_Controller.Fade.LoobyFadeOut();
        GameManager.Instance.SaveAllData();
    }

    public void CancleDecideLobby()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        decideObject.SetActive(false);
        int cnt = btns.Length;
        for (int i = 0; i < cnt; i++)
        {
            btns[i].interactable = true;
        }
    }
}
