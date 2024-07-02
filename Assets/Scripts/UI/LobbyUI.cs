using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField, Tooltip("0:시작, 1:나가기")] Button[] lobbyBtns;

    private void Awake()
    {
        lobbyBtns[(int)LobbyUIType.Start].onClick.AddListener(()=> { GameManager.LoadScene_Manager.LoadScene(nextSceneName); });
        lobbyBtns[(int)LobbyUIType.Exit].onClick.AddListener(()=> { Application.Quit(); });
    }
}
