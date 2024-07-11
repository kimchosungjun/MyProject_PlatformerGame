using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
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
    }
}
