using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region SerializeField 
    [SerializeField] Transform attackTransform;
    public Transform AttackTransform { get { return attackTransform; } }
    [SerializeField] PlayerType currentType;
    public PlayerType CurrentType { get { return currentType; } }
    #endregion

    Player[] players;
    public Player[] Players { get { return players; } }
    List<PlayerData> dataList = new List<PlayerData>();
    public List<PlayerData> DataList { get { return dataList; } }
    public float CurHP { get; set; }
    public float MaxHp { get; set; }
    public PlayerGroundCheck GroundChecker { get; set; }

    #region Unity Cycle
    private void Awake()
    {
        InitPlayerInformation();
    }

    public void InitPlayerInformation()
    {
        players = GetComponentsInChildren<Player>();
        int playerCnt = players.Length;
        for (int idx = 0; idx < playerCnt; idx++)
        {
            players[idx].Init(this);
            PlayerData data = players[idx].Data;
            dataList.Add(data);
        }
        
        GroundChecker = GetComponentInChildren<PlayerGroundCheck>();
        GroundChecker.Init(currentType);

        MaxHp = dataList[0].maxHp;
        CurHP = MaxHp;
    }

    private void Update()
    {
        InputPlayer();
        InputChangeType();
    }

    public void InputPlayer()
    {
        switch (currentType)
        {
            case PlayerType.Normal:
                players[0].Execute();
                break;
            default:
                break;
        }
    }

    public void InputChangeType()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (players[(int)currentType].IsGround)
            {
                // 추후에 원소 변경 시, 코드 작성
                // 땅에 닿아 있을때만 호출 가능
                //ChangeType();
            }
        }
    }
    #endregion



    public void ChangeType(PlayerType _playerType)
    {
        currentType = _playerType;
        GroundChecker.ChangeType(currentType);
        switch (_playerType)
        {
            case PlayerType.Normal:
                break;
            case PlayerType.Wind:
                break;
            case PlayerType.Water:
                break;
            default:
                break;
        }
    }
}
