using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyCntText;

    private void Awake()
    {
        enemyCntText.text = "0";
    }

    public void UpdateEnemyCnt(int _cnt)
    {
        enemyCntText.text = _cnt.ToString();
    }
}
