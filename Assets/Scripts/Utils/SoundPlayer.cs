using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clips;

    Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

    public void Init()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFXSound(string _name)
    {
        if (!clipDictionary.ContainsKey(_name))
        {
            AudioClip clip = GameManager.Instance.Sound_Manager.GetSFXSound(_name);
            if (clip == null)
                return;
            else
                clipDictionary.Add(clip.name, clip);
        }
        audioSource.PlayOneShot(clipDictionary[_name]);
    }
}
