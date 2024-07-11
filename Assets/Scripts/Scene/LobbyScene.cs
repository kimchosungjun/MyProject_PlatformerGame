using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    private void Start()
    {
        LoadScene();
    }

    public override void LoadNextScene()
    {
        GameManager.Instance.CurrentScene = null;
        GameObject go = GameObject.FindWithTag("Player");
        if (go == null)
            return;
        go.SetActive(true);
    }

    public override void LoadScene()
    {
        GameManager.Instance.CurrentScene = this;
        GameObject go = GameObject.FindWithTag("Player");
        if (go == null)
            return;
        go.SetActive(false);
    }
}
