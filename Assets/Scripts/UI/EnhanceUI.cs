using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceUI : MonoBehaviour
{
    [SerializeField, Tooltip("체력,구르기,공격력,버프")] Button[] selectEnhanceBtns;
    [SerializeField, Tooltip("Yes,No")] Button[] determineBnts;
    [SerializeField] GameObject determineUI;
    [SerializeField] GameObject warnUI;
    EnhanceType currentType = EnhanceType.None;
    EnhanceValueData data;
    void Awake()
    {
        int selectCnt = selectEnhanceBtns.Length;
        for(int idx=0; idx<selectCnt; idx++)
        {
            EnhanceType changeType = new EnhanceType();
            int curIdx = idx;
            switch (curIdx)
            {
                case 0:
                    changeType = EnhanceType.HP;
                    break;
                case 1:
                    changeType = EnhanceType.Roll;
                    break;
                case 2:
                    changeType = EnhanceType.Attack;
                    break;
                case 3:
                    changeType = EnhanceType.Buff;
                    break;
            }
            selectEnhanceBtns[(int)EnhanceType.HP].onClick.AddListener(() => SelectEnhanceBtnClick(changeType));
        }

        determineBnts[0].onClick.AddListener(() => { ClickYes(); });
        determineBnts[1].onClick.AddListener(() => { ClickNo(); });
    }

    public void SelectEnhanceBtnClick(EnhanceType _changeType)
    {
        if(data==null)
            data = GameManager.Instance.PlayerData_Manager.EnhanceData;
        if (data == null)
            Debug.Log("없슴미다!");
        currentType = _changeType;
        switch (_changeType)
        {
            case EnhanceType.HP:
                if (GameManager.Aum_Manager.CanUseAum((int)data.costs[data.hpDegree]) && GameManager.Instance.PlayerData_Manager.CanEnhance(_changeType))
                    ActiveDetermineUI();
                else
                    ActiveWarnUI();
                break;
            case EnhanceType.Roll:
                if (GameManager.Aum_Manager.CanUseAum((int)data.costs[data.rollDegree]) && GameManager.Instance.PlayerData_Manager.CanEnhance(_changeType))
                    ActiveDetermineUI();
                else
                    ActiveWarnUI();
                break;
            case EnhanceType.Attack:
                if (GameManager.Aum_Manager.CanUseAum((int)data.costs[data.attackDegree]) && GameManager.Instance.PlayerData_Manager.CanEnhance(_changeType))
                    ActiveDetermineUI();
                else
                    ActiveWarnUI();
                break;
            case EnhanceType.Buff:
                if (GameManager.Aum_Manager.CanUseAum((int)data.costs[data.buffDegree]) && GameManager.Instance.PlayerData_Manager.CanEnhance(_changeType))
                    ActiveDetermineUI();
                else
                    ActiveWarnUI();
                break;
        }   
    }

    public void ClickYes()
    {
        int cnt = selectEnhanceBtns.Length;
        for (int idx = 0; idx < cnt; idx++)
        {
            selectEnhanceBtns[idx].interactable = true;
        }

        switch (currentType)
        {
            case EnhanceType.HP:
                GameManager.Aum_Manager.UseAum((int)data.costs[data.hpDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType);
                break;
            case EnhanceType.Roll:
                GameManager.Aum_Manager.UseAum((int)data.costs[data.rollDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType);
                break;
            case EnhanceType.Attack:
                GameManager.Aum_Manager.UseAum((int)data.costs[data.attackDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType);
                break;
            case EnhanceType.Buff:
                GameManager.Aum_Manager.UseAum((int)data.costs[data.buffDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType);
                break;
        }

        determineUI.SetActive(false);
        currentType = EnhanceType.None;
    }

    public void ClickNo()
    {
        int cnt = selectEnhanceBtns.Length;
        for (int idx = 0; idx < cnt; idx++)
        {
            selectEnhanceBtns[idx].interactable = true;
        }
        determineUI.SetActive(false);
        currentType = EnhanceType.None;
    }

    public void ActiveDetermineUI()
    {
        int cnt = selectEnhanceBtns.Length;
        for(int idx=0; idx<cnt; idx++)
        {
            selectEnhanceBtns[idx].interactable = false;
        }
        determineUI.SetActive(true);
    }

    public void ActiveWarnUI()
    {
        StartCoroutine(WarnCor());
    }

    public IEnumerator WarnCor()
    {
        warnUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        warnUI.SetActive(false);
    }
}
