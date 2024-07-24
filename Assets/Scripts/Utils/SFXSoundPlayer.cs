using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    public void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerSFX(PlayerSoundType _soundType)
    {
        audioSource.PlayOneShot(clips[(int)_soundType]);
    }

    public void PlayEnemySFX(EnemySoundType _soundType)
    {
        audioSource.PlayOneShot(clips[(int)_soundType]);
    }
}
