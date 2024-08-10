using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField] string battleSceneName;
    [SerializeField, Tooltip("0:새로하기,1: 이어하기,  2:나가기")] Button[] lobbyBtns;
    [SerializeField] GameObject decideExitObject;
    [SerializeField] GameObject decideResetObject;
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

        lobbyBtns[(int)LobbyUIType.Exit].onClick.AddListener(()=> { ActiveDecideExitBtn(); });
    }

    public void BlockBtn()
    {
        if (!GameManager.Instance.PlayerData_Manager.CheckDataPath(GameManager.Instance.PlayerData_Manager.UseDataPath))
            lobbyBtns[(int)LobbyUIType.Continue].interactable = false;
        //Debug.Log(GameManager.Instance.PlayerData_Manager.UseDataPath);
    }

    public void ActiveResetDecideBtn()
    {
        if(lobbyBtns[(int)LobbyUIType.Continue].interactable == false)
        {
            ResetBtn();
        }
        else
        {
            GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
            int cnt = lobbyBtns.Length;
            for (int i = 0; i < cnt; i++)
            {
                lobbyBtns[i].interactable = false;
            }
            decideResetObject.SetActive(true);
        }
    }

    public void ExitResetBtn()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        int cnt = lobbyBtns.Length;
        for (int i = 0; i < cnt; i++)
        {
            lobbyBtns[i].interactable = true;
        }
        decideResetObject.SetActive(false);
    }

    public void ResetBtn()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        GameManager.LoadScene_Manager.LoadScene(nextSceneName); 
        GameManager.Instance.PlayerData_Manager.StartNew();
        GameManager.Instance.TalkData_Manager.StartNew();
        GameManager.Instance.PlayerData_Manager.CreateUseData();
    }

    public void ContinueBtn()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        GameManager.LoadScene_Manager.LoadScene(battleSceneName); 
        GameManager.Instance.PlayerData_Manager.StartContinue();
        GameManager.Instance.TalkData_Manager.StartContinue();
    }

    public void ActiveDecideExitBtn()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        decideExitObject.SetActive(true);
        int cnt = lobbyBtns.Length;
        for(int i=0; i<cnt; i++)
        {
            lobbyBtns[i].interactable = false;
        }
    }

    public void ExitGame()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        Application.Quit();
    }

    public void CancleExitGame()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        decideExitObject.SetActive(false);
        int cnt = lobbyBtns.Length;
        for (int i = 0; i < cnt; i++)
        {
            lobbyBtns[i].interactable = true;
        }
    }


}
