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
    private float curHp;
    public float CurHP { get { return curHp; } set { curHp = value;  GameManager.Instance.UI_Controller.HP.UpdateCurHpBar(curHp); } }
    private float maxHp;
    public float MaxHp { get { return maxHp; } set { maxHp = value; GameManager.Instance.UI_Controller.HP.UpdateMaxHPBar(maxHp,curHp); } }
    public PlayerGroundCheck GroundChecker { get; set; }

    private bool canControlPlayer = true;
    public bool CanControlPlayer { get { return canControlPlayer; } set { canControlPlayer = value; players[(int)currentType].CanControll = value; } }
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
        if (canControlPlayer)
        {
            InputChangeType();
        }
        InputPlayer();
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

    public void Heal(float _healValue)
    {
        if (_healValue + curHp > maxHp)
            CurHP = MaxHp;
        else
            CurHP += _healValue;
    }

    public void Hit(float _damage)
    {
        players[(int)currentType].Hit(_damage);
    }
}
