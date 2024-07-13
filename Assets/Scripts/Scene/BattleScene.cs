using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    [SerializeField] Vector3 playerStartPos;
    private void Start()
    {
        LoadScene();
    }

    public override void LoadNextScene()
    {
        GameManager.Instance.CurrentScene = null;
    }

    public override void LoadScene()
    {
        GameManager.Instance.CurrentScene = this;
        GameManager.Instance.Controller.gameObject.transform.position = playerStartPos;
        GameManager.Instance.UI_Controller.Fade.FadeIn();
    }
}
