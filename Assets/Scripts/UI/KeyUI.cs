using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField,Tooltip("Tab,Q,W,E")] Image[] keyImages;

    float rollTimer = 0f;
    float attackTimer = 0f;
    float buffTimer = 0f;
    float attackSkillTimer = 0f;

    /// <summary>
    /// Timer : 걸리는 시간, idx : 0(tab), 1(q), 2(w), 3(e)
    /// </summary>
    /// <param name="_timer"></param>
    /// <param name="_idx"></param>
    public void KeyPress(float _timer, PressKeyType _keyType)
    {
        StartCoroutine(KeyPressTimerCor(_timer, (int)_keyType));
    }

    public IEnumerator KeyPressTimerCor(float _timer, int _idx)
    {
        keyImages[_idx].fillAmount = 0;
        switch (_idx)
        {
            case 0: // roll
                rollTimer = 0f;
                while (rollTimer < _timer)
                {
                    rollTimer += Time.deltaTime;
                    keyImages[_idx].fillAmount = Mathf.Lerp(0, 1, rollTimer / _timer);
                    yield return null;
                }
                keyImages[_idx].fillAmount = 1;
                break;
            case 1: // attack
                attackTimer = 0f;
                while (attackTimer < _timer)
                {
                    attackTimer += Time.deltaTime;
                    keyImages[_idx].fillAmount = Mathf.Lerp(0, 1, attackTimer / _timer);
                    yield return null;
                }
                keyImages[_idx].fillAmount = 1;
                break;
            case 2: //buff
                buffTimer = 0f;
                while (buffTimer < _timer)
                {
                    buffTimer += Time.deltaTime;
                    keyImages[_idx].fillAmount = Mathf.Lerp(0, 1, buffTimer / _timer);
                    yield return null;
                }
                keyImages[_idx].fillAmount = 1;
                break;
            case 3: // attackskill
                attackSkillTimer = 0f;
                while (attackSkillTimer < _timer)
                {
                    attackSkillTimer += Time.deltaTime;
                    keyImages[_idx].fillAmount = Mathf.Lerp(0, 1, attackSkillTimer / _timer);
                    yield return null;
                }
                keyImages[_idx].fillAmount = 1;
                break;
        }
    }

    public void ResetCoolTime(PressKeyType _keyType)
    {
        keyImages[(int)_keyType].fillAmount = 1;
    }

    public void ChangeType()
    {

    }
   
}
