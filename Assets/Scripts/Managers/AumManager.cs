using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumManager 
{
    int haveAum = 0;
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
            return;
        haveAum -= _cost;
        GameManager.Instance.UI_Controller.Aum.UpdateAumAmount(haveAum);
        GameManager.Instance.UI_Controller.Enhance.UpdateHaveAumState(haveAum);
    }

    public void GetAum(int _earn)
    {
        haveAum += _earn;
        GameManager.Instance.UI_Controller.Aum.UpdateAumAmount(haveAum);
        GameManager.Instance.UI_Controller.Enhance.UpdateHaveAumState(haveAum);
    }

    public void Clear()
    {
        haveAum = 0;
    }
}
