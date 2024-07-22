using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField, Tooltip("0:새로하기,1: 이어하기,  2:나가기")] Button[] lobbyBtns;

    private void Awake()
    {
        float targetRatio = 9.0f / 16.0f;
        //float ratio = (float)Screen.height / (float)Screen.width;
        //float scaleHeight = ratio / targetRatio;
        float fixedWidth = (float)Screen.height / targetRatio;
        Screen.SetResolution((int)fixedWidth, Screen.height, true);
    }

    private void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        BlockBtn();

        //lobbyBtns[(int)LobbyUIType.Start].onClick.AddListener(()=> 
        //{ ResetBtn(); });
       
        //lobbyBtns[(int)LobbyUIType.Continue].onClick.AddListener(()=> 
        //{ ResetBtn();  });

        lobbyBtns[(int)LobbyUIType.Exit].onClick.AddListener(()=> { Application.Quit(); });
    }

    public void BlockBtn()
    {
        //if (!GameManager.Instance.PlayerData_Manager.CheckDataPath())
        //    lobbyBtns[(int)LobbyUIType.Continue].interactable = false;
    }

    public void ResetBtn()
    {
        GameManager.LoadScene_Manager.LoadScene(nextSceneName); 
        GameManager.Instance.PlayerData_Manager.StartNew();
        GameManager.Instance.TalkData_Manager.StartNew();
    }

    public void ContinueBtn()
    {
        GameManager.LoadScene_Manager.LoadScene(nextSceneName); 
        GameManager.Instance.PlayerData_Manager.StartContinue();
        GameManager.Instance.TalkData_Manager.StartContinue();
    }
}
