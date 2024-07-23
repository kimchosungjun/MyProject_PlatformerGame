using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] allSounds;
    [SerializeField] AudioSource bgmSource;
    [SerializeField, Tooltip("0:Master, 1:BGM, 2:SFX")] AudioMixerGroup[] mixers;

    Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    #region Link Sound
    public void Init()
    {
        int soundCnt = allSounds.Length;
        for(int idx=0; idx< soundCnt; idx++)
        {
            soundDictionary.Add(allSounds[idx].name, allSounds[idx]);
        }
    }

    public AudioClip GetSFXSound(string _name)
    {
        if (soundDictionary.ContainsKey(_name))
            return soundDictionary[_name];
        return null;
    }
    #endregion

    #region Set Sound
    public void ChangeBGM(string _name)
    {
        if (!soundDictionary.ContainsKey(_name))
        {
            Debug.LogError("해당 사운드가 없습니다.");
            return;
        }
        bgmSource.clip = soundDictionary[_name];
        bgmSource.Play();
    }

    public void SetVolume(MixerType _mixerType, float _volume)
    {
        if (_volume <= 0)
            _volume = 0.01f;
        string mixerName = ConvertEnum.ConvertEnumToString<MixerType>(_mixerType);
        mixers[((int)_mixerType)].audioMixer.SetFloat(mixerName, Mathf.Log10(_volume) * 20);
    }
    #endregion
}
