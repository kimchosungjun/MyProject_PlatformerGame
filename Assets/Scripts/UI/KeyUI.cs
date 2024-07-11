using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField,Tooltip("Tab,Q,W,E")] Image[] keyImages;

    float timer = 0f;

    /// <summary>
    /// Timer : 걸리는 시간, idx : 0(tab), 1(q), 2(w), 3(e)
    /// </summary>
    /// <param name="_timer"></param>
    /// <param name="_idx"></param>
    public void KeyPress(float _timer, int _idx)
    {
        StartCoroutine(KeyPressTimerCor(_timer, _idx));
    }

    public IEnumerator KeyPressTimerCor(float _timer, int _idx)
    {
        timer = 0f;
        keyImages[_idx].fillAmount = 0;
        while (timer < _timer)
        {
            timer += Time.deltaTime;
            keyImages[_idx].fillAmount = Mathf.Lerp(0, 1, timer / _timer);
            yield return null;
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            KeyPress(15f, 0);
        }
    }
}
