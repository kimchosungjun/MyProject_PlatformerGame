using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    Dictionary<string, AudioClip> soundDictionary;
    public Dictionary<string, AudioClip> SoundDictionary { get { return soundDictionary; } set { soundDictionary = value; } }

}
