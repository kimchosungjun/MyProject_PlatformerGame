using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource uiSource;
    [SerializeField, Tooltip("0은 클릭, 1은 경고")] AudioClip[] uiClips;

    #region Set Sound
    public void PlayBGM(AudioClip _clip)
    {
        bgmSource.clip = _clip;
        bgmSource.Play();
    }

    public void PlayUISFX(UISoundType _soundType)
    {
        uiSource.PlayOneShot(uiClips[(int)_soundType]);
    }
    #endregion
}
