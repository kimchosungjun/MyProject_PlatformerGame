using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager 
{
    public void LoadScene(string _sceneName)
    {
        GameManager.Instance.CurrentScene.LoadNextScene();
        SceneManager.LoadScene(_sceneName);
        //GameManager.Instance.ClearManagers();
        //GameManager.Instance.InitManagers();
    }
}
