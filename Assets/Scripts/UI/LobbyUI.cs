using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField, Tooltip("0:�����ϱ�,1: �̾��ϱ�,  2:������")] Button[] lobbyBtns;

    [SerializeField] List<NPCTalkData> talkDataList = new List<NPCTalkData>();

    private void Start()
    {
        BlockBtn();

        lobbyBtns[(int)LobbyUIType.Start].onClick.AddListener(()=> 
        { GameManager.LoadScene_Manager.LoadScene(nextSceneName);  GameManager.Instance.PlayerData_Manager.StartNew(); InitTalkDatas(); });
       
        lobbyBtns[(int)LobbyUIType.Continue].onClick.AddListener(()=> 
        { GameManager.LoadScene_Manager.LoadScene(nextSceneName); GameManager.Instance.PlayerData_Manager.StartContinue(); });

        lobbyBtns[(int)LobbyUIType.Exit].onClick.AddListener(()=> { Application.Quit(); });
    }

    public void BlockBtn()
    {
        if (!GameManager.Instance.PlayerData_Manager.CheckDataPath())
            lobbyBtns[(int)LobbyUIType.Continue].interactable = false;
    }

    public void InitTalkDatas()
    {
        int cnt = talkDataList.Count;
        for(int i=0; i<cnt; i++)
        {
            talkDataList[i].Init();
        }
    }
}
