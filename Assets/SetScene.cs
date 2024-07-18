using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScene : MonoBehaviour
{
    [SerializeField, Tooltip("0 : �κ�, 1: Ʃ�丮��, 2: ��Ʋ, 3: Ÿ��, 4: ����")] GameObject[] sceneObject;
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
