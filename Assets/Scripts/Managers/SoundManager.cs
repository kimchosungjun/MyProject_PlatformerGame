using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource uiSource;
    [SerializeField, Tooltip("0�� Ŭ��, 1�� ���")] AudioClip[] uiClips;

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

    public void StopBGM()
    {
        bgmSource.Stop();
    }
    #endregion

    #region Save Data
    public float masterValue=1f;
    public float bgmValue=1f;
    public float sfxValue=1f;

    #endregion
}
