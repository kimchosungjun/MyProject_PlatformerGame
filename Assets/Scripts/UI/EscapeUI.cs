using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EscapeUI : MonoBehaviour
{
    public bool IsOn { get; set; } = false;
    /// <summary>
    /// IsOn�� ������ false, true ȣ������� ��
    /// </summary>
    /// <param name="_isActive"></param>
    public abstract void TurnOnOffUI(bool _isActive);
}
