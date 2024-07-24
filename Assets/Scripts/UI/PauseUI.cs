using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : EscapeUI
{
    [SerializeField] GameObject pauseObject;
    
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

    public void ExitGame()
    {
        GameManager.Instance.SaveAllData();
        Application.Quit();
    }

    public void PressSoundManage()
    {
        GameManager.Instance.UI_Controller.Sound.TurnOnOffUI(true);
    }
}
