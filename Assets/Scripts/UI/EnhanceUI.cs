using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnhanceUI : EscapeUI
{
    [SerializeField, Tooltip("체력,구르기,공격력,버프")] Button[] selectEnhanceBtns;
    [SerializeField, Tooltip("Yes,No,Exit")] Button[] determineBnts;
    [SerializeField] GameObject determineUI;
    [SerializeField] GameObject warnUI;
    [SerializeField] GameObject onoffObject;
    [SerializeField] TextMeshProUGUI aumText;
    [SerializeField] TextMeshProUGUI[] costTexts;
    [SerializeField] TextMeshProUGUI determineWarnText;
    [SerializeField] TextMeshProUGUI buffWarnText;
    EnhanceType currentType = EnhanceType.None;
    EnhanceValueData enhanceData;
    PlayerData playerData;

    bool[] btnCanInteractable = new bool[4] { true,true,true,true};
    bool isShowWarnMessage = false;
    bool isShowBuffWarnMessage = false;
    public bool IsUseBuff { get; set; } = false;

    #region Life Cycle

    void Awake()
    {
        selectEnhanceBtns[(int)EnhanceType.HP].onClick.AddListener(() => { HpEnhanceClick(); });
        selectEnhanceBtns[(int)EnhanceType.Roll].onClick.AddListener(() => { RollEnhanceClick(); });
        selectEnhanceBtns[(int)EnhanceType.Attack].onClick.AddListener(() => { AttackEnhanceClick(); });
        selectEnhanceBtns[(int)EnhanceType.Buff].onClick.AddListener(() => { BuffEnhanceClick(); });

        determineBnts[0].onClick.AddListener(() => { ClickYes(); });
        determineBnts[1].onClick.AddListener(() => { ClickNo(); });
        determineBnts[2].onClick.AddListener(() => { TurnOnOffUI(false); } );
    }

    private void Start()
    {
        aumText.text = GameManager.Aum_Manager.HaveAum.ToString();
        if (enhanceData == null)
            enhanceData = GameManager.Instance.PlayerData_Manager.EnhanceData;
        if (playerData == null)
            playerData = GameManager.Instance.PlayerData_Manager.PData;
        CheckSelectBtnActiveState();
    }

    public void CheckSelectBtnActiveState()
    {
        if (!GameManager.Instance.PlayerData_Manager.CanEnhance(EnhanceType.HP))
        {
            btnCanInteractable[0] = false;
            selectEnhanceBtns[0].interactable = false;
            costTexts[0].text = "-";
        }
        else
        {
            UpdateBtnState(0, true);
        }

        if (!GameManager.Instance.PlayerData_Manager.CanEnhance(EnhanceType.Roll))
        {
            btnCanInteractable[1] = false;
            selectEnhanceBtns[1].interactable = false;
            costTexts[1].text = "-";
        }
        else
        {
            UpdateBtnState(1, true);
        }

        if (!GameManager.Instance.PlayerData_Manager.CanEnhance(EnhanceType.Attack))
        {
            btnCanInteractable[2] = false;
            selectEnhanceBtns[2].interactable = false;
            costTexts[2].text = "-";
        }
        else
        {
            UpdateBtnState(2, true);
        }

        if (!GameManager.Instance.PlayerData_Manager.CanEnhance(EnhanceType.Buff))
        {
            btnCanInteractable[3] = false;
            selectEnhanceBtns[3].interactable = false;
            costTexts[3].text = "-";
        }
        else 
        {
            UpdateBtnState(3, true);
        }
    }
    #endregion

    #region EnhanceBtn Event
    public void HpEnhanceClick()
    {
        if (enhanceData == null)
            enhanceData = GameManager.Instance.PlayerData_Manager.EnhanceData;
        currentType = EnhanceType.HP;
        if (GameManager.Instance.PlayerData_Manager.CanEnhance(currentType) && GameManager.Aum_Manager.CanUseAum((int)enhanceData.aumCost[playerData.curHpDegree]))
            ActiveDetermineUI();
        else
            ActiveWarnUI();
    }

    public void RollEnhanceClick()
    {
        if (enhanceData == null)
            enhanceData = GameManager.Instance.PlayerData_Manager.EnhanceData;
        currentType = EnhanceType.Roll;
        if (GameManager.Instance.PlayerData_Manager.CanEnhance(currentType) && GameManager.Aum_Manager.CanUseAum((int)enhanceData.aumCost[playerData.curRollDegree]))
            ActiveDetermineUI();
        else
            ActiveWarnUI();
    }

    public void AttackEnhanceClick()
    {
        if (enhanceData == null)
            enhanceData = GameManager.Instance.PlayerData_Manager.EnhanceData;
        currentType = EnhanceType.Attack;
        if (GameManager.Instance.PlayerData_Manager.CanEnhance(currentType) && GameManager.Aum_Manager.CanUseAum((int)enhanceData.aumCost[playerData.curAttackDegree]))
            ActiveDetermineUI();
        else
            ActiveWarnUI();
    }

    public void BuffEnhanceClick()
    {
        if (enhanceData == null)
            enhanceData = GameManager.Instance.PlayerData_Manager.EnhanceData;
        currentType = EnhanceType.Buff;
        if (GameManager.Instance.PlayerData_Manager.CanEnhance(currentType) && GameManager.Aum_Manager.CanUseAum((int)enhanceData.aumCost[playerData.curBuffDegree]))
            ActiveDetermineUI();
        else
            ActiveWarnUI();
    }
    #endregion

    #region Show Determine UI & Warn UI
    public void ActiveDetermineUI()
    {
        int cnt = selectEnhanceBtns.Length;
        for (int idx = 0; idx < cnt; idx++)
        {
            selectEnhanceBtns[idx].interactable = false;
        }
        warnUI.SetActive(false);
        isShowWarnMessage = false;
        switch (currentType)
        {
            case EnhanceType.HP:
                determineWarnText.text = "HP를 강화하시겠습니까?";
                break;
            case EnhanceType.Roll:
                determineWarnText.text = "구르기를 강화하시겠습니까?";
                break;
            case EnhanceType.Attack:
                determineWarnText.text = "공격력을 강화하시겠습니까?";
                break;
            case EnhanceType.Buff:
                determineWarnText.text = "버프를 강화하시겠습니까?";
                break;
        }
        determineUI.SetActive(true);
    }

    public void ActiveWarnUI()
    {
        if (!isShowWarnMessage)
        {
            warnUI.SetActive(true);
            isShowWarnMessage = true;
        }
    }

    public void ShowBuffWarnMessage()
    {
        if (!isShowBuffWarnMessage)
            StartCoroutine(BuffWarnMessageCor());
    }

    public IEnumerator BuffWarnMessageCor()
    {
        float warnTimer = 0f;
        Color color = buffWarnText.color;
        color.a = 0f;
        buffWarnText.color = color;
        isShowBuffWarnMessage = true;
        while (warnTimer < 2f)
        {
            warnTimer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, warnTimer / 2f);
            buffWarnText.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        color.a = 0f;
        buffWarnText.color = color;
        isShowBuffWarnMessage = false;
    }
    #endregion

    #region Select Enhance
    public void ClickYes()
    {
        switch (currentType)
        {
            case EnhanceType.HP:
                GameManager.Aum_Manager.UseAum((int)enhanceData.aumCost[playerData.curHpDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType, this);
                break;
            case EnhanceType.Roll:
                GameManager.Aum_Manager.UseAum((int)enhanceData.aumCost[playerData.curRollDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType, this);
                break;
            case EnhanceType.Attack:
                GameManager.Aum_Manager.UseAum((int)enhanceData.aumCost[playerData.curAttackDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType, this);
                break;
            case EnhanceType.Buff:
                GameManager.Aum_Manager.UseAum((int)enhanceData.aumCost[playerData.curBuffDegree]);
                GameManager.Instance.PlayerData_Manager.ApplyEnhanceStat(currentType, this);
                break;
        }
       
        determineUI.SetActive(false);
        currentType = EnhanceType.None;

        CheckSelectBtnActiveState();
    }

    public void ClickNo()
    {
        int cnt = selectEnhanceBtns.Length;
        for (int idx = 0; idx < cnt; idx++)
        {
            if(btnCanInteractable[idx])
                selectEnhanceBtns[idx].interactable = true;
        }
        determineUI.SetActive(false);
        currentType = EnhanceType.None;
    }

    public void UpdateBtnState(int _idx, bool _isInteractable)
    {
        int cnt = selectEnhanceBtns.Length;
        btnCanInteractable[_idx] = _isInteractable;
        if(!_isInteractable)
            costTexts[_idx].text = "-";
        else
        {
            switch (_idx)
            {
                case 0:
                    costTexts[_idx].text = enhanceData.aumCost[playerData.curHpDegree].ToString();
                    break;
                case 1:
                    costTexts[_idx].text = enhanceData.aumCost[playerData.curRollDegree].ToString();
                    break;
                case 2:
                    costTexts[_idx].text = enhanceData.aumCost[playerData.curAttackDegree].ToString();
                    break;
                case 3:
                    costTexts[_idx].text = enhanceData.aumCost[playerData.curBuffDegree].ToString();
                    break;
            }
        }
        for (int idx=0; idx<cnt; idx++)
        {
            if (btnCanInteractable[idx])
                selectEnhanceBtns[idx].interactable = true;
            else
                selectEnhanceBtns[idx].interactable = false;
        }
    }

    public void UpdateHaveAumState(int _haveAum)
    {
        aumText.text = _haveAum.ToString();
    }
    #endregion

    #region OnOffUI
    public override void TurnOnOffUI(bool _isActive)
    {
        if (_isActive)
        {
            IsOn = true;
            aumText.text = GameManager.Aum_Manager.HaveAum.ToString();
            onoffObject.SetActive(_isActive);
            Time.timeScale = 0f;
        }
        else
        {
            if(determineUI.activeSelf)
            {
                ClickNo();
                return;
            }
            IsOn = false;
            onoffObject.SetActive(_isActive);
            Time.timeScale = 1f;
            warnUI.SetActive(false);
        }
    }
    #endregion
}
