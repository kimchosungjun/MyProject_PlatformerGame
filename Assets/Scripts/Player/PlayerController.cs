using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerType currentType;

    [SerializeField] NormalPlayer normalPlayer;

    #region Unity Cycle
    private void Awake()
    {
        currentType = PlayerType.Normal;
        if (normalPlayer != null)
            normalPlayer.Init(this);
    }

    private void Update()
    {
        if (normalPlayer != null)
        {
            // 추후에 다른 원소 타입 추가 시, Switch문으로 분기 만들어서 실행시키기
            normalPlayer.Execute();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 추후에 원소 변경 시, 코드 작성
            // 땅에 닿아 있을때만 호출 가능
        }
    }
    #endregion
}
