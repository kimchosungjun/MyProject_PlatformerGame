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

        slider[0].value = GameManager.Instance.Sound_Manager.masterValue;
        slider[1].value = GameManager.Instance.Sound_Manager.bgmValue;
        slider[2].value = GameManager.Instance.Sound_Manager.sfxValue;

        if (slider[0].value <= 0.001f)
        {
            if (!isClickMasterBtn)
                beforeMasterValue = slider[0].maxValue;
            isClickMasterBtn = true;
            offObject[0].SetActive(true);
        }

        if (slider[1].value <= 0.001f)
        {
            if (!isClickBGMBtn)
                beforeBGMValue = slider[1].maxValue;
            isClickBGMBtn = true;
            offObject[1].SetActive(true);
        }

        if (slider[2].value <= 0.001f)
        {
            if (!isClickSFXBtn)
                beforeSFXValue = slider[2].maxValue;
            isClickSFXBtn = true;
            offObject[2].SetActive(true);
        }
    }

    #region Set Volume
    public void SetMasterVolume(float _value)
    {
        if (slider[0].value <= 0.001f)
        {
            if (!isClickMasterBtn)
                beforeMasterValue = slider[0].maxValue;
            isClickMasterBtn = true;
            offObject[0].SetActive(true);
        }
        else if (isClickMasterBtn && _value != slider[0].minValue)
        {
            isClickMasterBtn = false;
            offObject[0].SetActive(false);
        }

        GameManager.Instance.Sound_Manager.masterValue = _value;
        SetVolume(MixerType.Master,_value);
    }

    public void SetBGMVolume(float _value)
    {
        if (slider[1].value <= 0.001f)
        {
            if (!isClickBGMBtn)
                beforeBGMValue = slider[1].maxValue;
            isClickBGMBtn = true;
            offObject[1].SetActive(true);
        }
        else if (isClickBGMBtn && _value != slider[1].minValue)
        {
            isClickBGMBtn = false;
            offObject[1].SetActive(false);
        }

        GameManager.Instance.Sound_Manager.bgmValue = _value;
        SetVolume(MixerType.BGM, _value);
    }

    public void SetSFXVolume(float _value)
    {
        if (slider[2].value <= 0.001f)
        {
            if(!isClickSFXBtn)
                beforeSFXValue = slider[2].maxValue;
            isClickSFXBtn = true;
            offObject[2].SetActive(true);
        }
        else if(isClickSFXBtn && _value !=slider[2].minValue)
        {
            isClickSFXBtn = false;
            offObject[2].SetActive(false);
        }

        GameManager.Instance.Sound_Manager.sfxValue = _value;
        SetVolume(MixerType.SFX, _value);
    }

    public void SetVolume(MixerType _mixerType, float _volume)
    {
        string mixerName = ConvertEnum.ConvertEnumToString<MixerType>(_mixerType);
        mixer.SetFloat(mixerName, Mathf.Log10(_volume) * 20);
    }
    #endregion

    #region Sound Button
    [SerializeField] bool isClickMasterBtn = false;
    [SerializeField] bool isClickBGMBtn = false;
    [SerializeField] bool isClickSFXBtn = false;

    [SerializeField] float beforeMasterValue;
    [SerializeField] float beforeBGMValue;
    [SerializeField] float beforeSFXValue;

    [SerializeField, Tooltip("Master/BGM/SFX 순서대로")] Slider[] slider;
    [SerializeField, Tooltip("Master/BGM/SFX 순서대로")] GameObject[] offObject;

    public void ClickMasterBtn()
    {
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        if (isClickMasterBtn)
        {
            offObject[0].SetActive(false);
            isClickMasterBtn = !isClickMasterBtn;
            slider[0].value = beforeMasterValue;
            return;
        }
        else
        {
            offObject[0].SetActive(true);
            isClickMasterBtn = !isClickMasterBtn;
            beforeMasterValue = slider[0].value;
            slider[0].value = slider[0].minValue;
            return;
        }
    }

    public void ClickBGMBtn()
    {
        if (isClickBGMBtn)
        {
            offObject[1].SetActive(false);
            isClickBGMBtn = !isClickBGMBtn;
            slider[1].value = beforeBGMValue;
            return;
        }
        else
        {
            offObject[1].SetActive(true);
            isClickBGMBtn = !isClickBGMBtn;
            beforeBGMValue = slider[1].value;
            slider[1].value = slider[1].minValue;
            return;
        }
    }

    public void ClickSFXBtn()
    {
        if (isClickSFXBtn)
        {
            offObject[2].SetActive(false);
            isClickSFXBtn = !isClickSFXBtn;
            slider[2].value = beforeSFXValue;
            return;
        }
        else
        {
            offObject[2].SetActive(true);
            isClickSFXBtn = !isClickSFXBtn;
            beforeSFXValue = slider[2].value;
            slider[2].value = slider[2].minValue;
            return;
        }
    }
    #endregion

    public override void TurnOnOffUI(bool _isActive)
    {
        IsOn = _isActive;
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        soundUIObject.SetActive(_isActive);
    }
}
