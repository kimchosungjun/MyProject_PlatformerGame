using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    [SerializeField] Vector3 playerSpawnPos;
    private void Start()
    {
        LoadScene();
    }

    public override void LoadNextScene()
    {
        GameManager.Instance.CurrentScene = null;
        GameManager.Instance.Controller.gameObject.transform.position = playerSpawnPos;
    }

    public override void LoadScene()
    {
        GameManager.Instance.CurrentScene = this;
        GameManager.Instance.UI_Controller.Fade.FadeIn();
        GameManager.Instance.Controller.LoadHP();
        GameManager.Instance.Sound_Manager.PlayBGM(bgmClip);
    }
}
