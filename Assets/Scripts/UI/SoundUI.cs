using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundUI : EscapeUI
{
    [SerializeField, Tooltip("0:Master, 1:BGM, 2:SFX")] AudioMixer mixer;
    [SerializeField] GameObject soundUIObject;

    private void Awake()
    {
        slider[0].onValueChanged.AddListener(SetMasterVolume);
        slider[1].onValueChanged.AddListener(SetBGMVolume);
        slider[2].onValueChanged.AddListener(SetSFXVolume);
    }

    #region Set Volume
    public void SetMasterVolume(float _value)
    {
        if (slider[0].value == slider[0].minValue)
        {
            offObject[0].SetActive(true);
            isClickMasterBtn = true;
        }
        else if (beforeMasterValue == slider[0].minValue)
        {
            offObject[0].SetActive(false);
            isClickMasterBtn = false;
        }
        beforeMasterValue = _value;
        SetVolume(MixerType.Master,_value);
    }

    public void SetBGMVolume(float _value)
    {
        if (slider[1].value == slider[1].minValue)
        {
            offObject[1].SetActive(true);
            isClickBGMBtn = true;
        }
        else if (beforeBGMValue == slider[1].minValue)
        {
            offObject[1].SetActive(false);
            isClickBGMBtn = false;
        }
        beforeBGMValue = _value;
        SetVolume(MixerType.BGM, _value);
    }

    public void SetSFXVolume(float _value)
    {
        if (slider[2].value == slider[2].minValue)
        {
            offObject[2].SetActive(true);
            isClickSFXBtn = true;
        }
        else if(beforeSFXValue == slider[2].minValue)
        {
            offObject[2].SetActive(false);
            isClickSFXBtn = false;
        }
        beforeSFXValue = _value;
        SetVolume(MixerType.SFX, _value);
    }

    public void SetVolume(MixerType _mixerType, float _volume)
    {
        string mixerName = ConvertEnum.ConvertEnumToString<MixerType>(_mixerType);
        mixer.SetFloat(mixerName, Mathf.Log10(_volume) * 20);
    }
    #endregion

    #region Sound Button
    bool isClickMasterBtn = false;
    bool isClickBGMBtn = false;
    bool isClickSFXBtn = false;

    float beforeMasterValue;
    float beforeBGMValue;
    float beforeSFXValue;

    [SerializeField, Tooltip("Master/BGM/SFX 순서대로")] Slider[] slider;
    [SerializeField, Tooltip("Master/BGM/SFX 순서대로")] GameObject[] offObject;

    public void ClickMasterBtn()
    {
        if (isClickMasterBtn)
        {
            offObject[0].SetActive(false);
            slider[0].value = beforeMasterValue;
        }
        else
        {
            offObject[0].SetActive(true);
            beforeMasterValue = slider[0].value;
            slider[0].value = slider[0].minValue;
        }
        isClickMasterBtn = !isClickMasterBtn;
    }

    public void ClickBGMBtn()
    {
        if (isClickBGMBtn)
        {
            offObject[1].SetActive(false);
            slider[1].value = beforeBGMValue;
        }
        else
        {
            offObject[1].SetActive(true);
            beforeBGMValue = slider[1].value;
            slider[1].value = slider[1].minValue;
        }
        isClickBGMBtn = !isClickBGMBtn;
    }

    public void ClickSFXBtn()
    {
        if (isClickSFXBtn)
        {
            offObject[2].SetActive(false);
            slider[2].value = beforeSFXValue;
        }
        else
        {
            offObject[2].SetActive(true);
            beforeSFXValue = slider[2].value;
            slider[2].value = slider[2].minValue;
        }
        isClickSFXBtn = !isClickSFXBtn;
    }
    #endregion

    public override void TurnOnOffUI(bool _isActive)
    {
        IsOn = _isActive;
        soundUIObject.SetActive(_isActive);
    }
}
