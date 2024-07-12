using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] EnhanceUI enhanceUI;
    public EnhanceUI Enhance { get { return enhanceUI; } set { enhanceUI = value; } }

    [SerializeField] HPUI hpUI;
    public HPUI HP { get { return hpUI; } set { hpUI = value; } }

    [SerializeField] EnemyUI enemyUI;
    public EnemyUI Enemy { get { return enemyUI; } set { enemyUI = value; } }

    [SerializeField] AumUI aumUI;
    public AumUI Aum { get { return aumUI; } set { aumUI = value; } }

    [SerializeField] IndicatorUI indicatorUI;
    public IndicatorUI Indicator { get { return indicatorUI; } set { indicatorUI = value; } }

    [SerializeField] KeyUI keyUI;
    public KeyUI Key { get { return keyUI; } set { keyUI = value; } }

    [SerializeField] List<EscapeUI> escapeUIList = new List<EscapeUI>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseUI();
    }

    public void CloseUI()
    {
        int cnt = escapeUIList.Count;
        for(int idx=cnt-1; idx>=0; idx--)
        {
            if (escapeUIList[idx].IsOn)
            {
                escapeUIList[idx].TurnOnOffUI(false);
                return;
            }
        }
    }
}
