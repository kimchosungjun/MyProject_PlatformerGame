using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] GameObject gameoverObject;

    public void GameOverActionMethod()
    {
        StartCoroutine(GameManager.Instance.UI_Controller.Fade.GameOverFadeOutCor());
    }

    public void TurnOnGameOver()
    {
        gameoverObject.SetActive(true);
    }

    public void ReturnLobby()
    {
        gameoverObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.SaveAllData();
        GameManager.LoadScene_Manager.LoadScene("Lobby");
    }
}
