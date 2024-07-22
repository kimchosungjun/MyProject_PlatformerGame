using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EscapeUI : MonoBehaviour
{
    public bool IsOn { get; set; } = false;
    /// <summary>
    /// IsOn을 무조건 false, true 호출해줘야 함
    /// </summary>
    /// <param name="_isActive"></param>
    public abstract void TurnOnOffUI(bool _isActive);
}
