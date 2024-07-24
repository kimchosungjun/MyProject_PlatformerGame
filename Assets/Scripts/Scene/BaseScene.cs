using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    [SerializeField] protected AudioClip bgmClip;
    public abstract void LoadNextScene();
    public abstract void LoadScene();

    public BossScene bossScene;
}
