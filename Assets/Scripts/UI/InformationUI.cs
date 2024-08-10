using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationUI : MonoBehaviour
{
    //Vector3 upVec = new Vector3(0, 100, 0);

    //[SerializeField] Canvas canvas;
    //[SerializeField] RectTransform infoRectTf;
    [SerializeField] GameObject infoObject;
    [SerializeField] TextMeshProUGUI infoText;

    [SerializeField, TextArea] string LShiftInfoStr;
    [SerializeField, TextArea] string QInfoStr;
    [SerializeField, TextArea] string WInfoStr;
    [SerializeField, TextArea] string EInfoStr;

    //public bool IsActive { get { return isActive; } set { isActive = value; } }

    public void PointEnter(string _type)
    {
        PlayerController controller = GameManager.Instance.Controller;
        PlayerData data = controller.Players[(int)controller.CurrentType].PData;
        GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        //Vector3 mousePosition = Input.mousePosition + upVec;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    canvas.transform as RectTransform,
        //    mousePosition,
        //    canvas.worldCamera,
        //    out Vector2 localPoint
        //);
        //infoRectTf.anchoredPosition = localPoint;

        switch (_type)
        {
            case "LShift":
                infoText.text = LShiftInfoStr + $"\n\n <color=red>ÄðÅ¸ÀÓ : {data.rollCoolTime}ÃÊ</color>";
                break;
            case "Q":
                infoText.text = QInfoStr + $"\n\n <color=red>ÄðÅ¸ÀÓ : {data.attackCoolTime}ÃÊ</color>";
                break;
            case "W":
                infoText.text = WInfoStr + $"\n\n <color=red>ÄðÅ¸ÀÓ : {data.buffSkillCoolTime}ÃÊ</color>";
                break;
            case "E":
                infoText.text = EInfoStr + $"\n\n <color=red>ÄðÅ¸ÀÓ : {data.attackSkillCoolTime}ÃÊ</color>";
                break;
        }
        infoObject.SetActive(true);
    }

    public void PointExit()
    {
        infoObject.SetActive(false);
    }
}
