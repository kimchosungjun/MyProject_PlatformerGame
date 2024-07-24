using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : BaseScene
{
    [SerializeField] GameObject afterKillBossTalkObj;
    [SerializeField] GameObject fightBoss;
    [SerializeField] GameObject imageBoss;

    [SerializeField] string bgmName;
    private void Start()
    {
        LoadScene();
    }

    public override void LoadNextScene()
    {
        GameManager.Instance.CurrentScene = null;
        bossScene = this;
    }

    public override void LoadScene()
    {
        GameManager.Instance.CurrentScene = this;
        GameManager.Instance.UI_Controller.Fade.FadeIn();
        GameManager.Instance.Controller.LoadHP();
        GameManager.Instance.Sound_Manager.PlayBGM(bgmClip);
    }

    public void AfterKillBoss()
    {
        afterKillBossTalkObj.SetActive(true);
    }

    public void DeleteAfterKillBoss()
    {
        afterKillBossTalkObj.SetActive(false);
    }

    public void StartBossBattle()
    {
        fightBoss.SetActive(true);
        imageBoss.SetActive(false);
    }
}
