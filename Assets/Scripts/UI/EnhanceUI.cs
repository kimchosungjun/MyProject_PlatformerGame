using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceUI : MonoBehaviour
{
    [SerializeField, Tooltip("체력,구르기,공격력,버프")] Button[] btns;
    EnhanceBtnType currentType = EnhanceBtnType.None;

    public void EnhanceClick()
    {

    }
}
