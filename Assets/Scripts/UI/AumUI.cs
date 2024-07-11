using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AumUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI aumCntText;

    private void Start()
    {
        aumCntText.text = GameManager.Aum_Manager.HaveAum.ToString();
    }

    public void UpdateAumAmount(int _cnt)
    {
        aumCntText.text = _cnt.ToString();
    }
}
