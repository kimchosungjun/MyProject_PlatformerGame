using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Link UI
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

    [SerializeField] DialogueUI dialogueUI;
    public DialogueUI Dialogue { get { return dialogueUI; } set { dialogueUI = value; } }
    #endregion

    [SerializeField] List<EscapeUI> escapeUIList = new List<EscapeUI>();

    [SerializeField] TutorialUI tutorialUI;
    public TutorialUI Tutorial { get { return tutorialUI; } set { tutorialUI = value; } }

    [SerializeField] FadeUI fadeUI;
    public FadeUI Fade { get { return fadeUI; } set { fadeUI = value; } }

    [SerializeField] InformationUI infoUI;
    public InformationUI Info { get { return infoUI; } set { infoUI = value; } }


    bool isPause = false;
    [SerializeField] PauseUI pauseUI;
    public PauseUI Pause { get { return pauseUI; } set { pauseUI = value; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseUI();
        Dialogue.Execute();
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

        if (isPause)
            pauseUI.TurnOnOffUI(false);
        else
            pauseUI.TurnOnOffUI(true);
        isPause = !isPause;
    }
}
