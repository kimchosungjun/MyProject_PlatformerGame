using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumManager 
{
    int haveAum = 100;
    public int HaveAum { get { return haveAum; } }

    public bool CanUseAum(int _cost)
    {
        if (haveAum - _cost < 0)
            return false;
        return true;
    }

    public void UseAum(int _cost)
    {
        if (!CanUseAum(_cost))
        {
            Debug.LogError("사용불가!");
            return;
        }
        haveAum -= _cost;
        GameManager.Instance.UI_Controller.Enhance.UpdateHaveAumState(haveAum);
    }

    public void GetAum(int _earn)
    {
        haveAum += _earn;
    }
}
