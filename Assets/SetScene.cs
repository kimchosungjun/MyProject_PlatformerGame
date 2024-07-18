using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScene : MonoBehaviour
{
    [SerializeField, Tooltip("0 : 로비, 1: 튜토리얼, 2: 배틀, 3: 타운, 4: 보스")] GameObject[] sceneObject;
    [SerializeField] SceneType currentScene;
    private void Awake()
    {
        ActiveScene((int)currentScene);
    }

    public void ActiveScene(int _idx)
    {
        int cnt = sceneObject.Length;
        for(int idx=0; idx<cnt; idx++)
        {
            if (idx == _idx)
            {
                sceneObject[idx].SetActive(true);
                return;
            }
        }
    }
}
