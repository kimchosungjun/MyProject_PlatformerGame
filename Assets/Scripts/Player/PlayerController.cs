using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region SerializeField 
    // Use Object : Attack, Heal, .. Effect..
    [SerializeField] ParticleSystem healParticle;
    [SerializeField] Transform attackTransform;
    public Transform AttackTransform { get { return attackTransform; } }
    // Type : Normal, Wind, Water
    [SerializeField] PlayerType currentType;
    public PlayerType CurrentType { get { return currentType; } }
    // Player : Normal, Wind, Water
    [SerializeField] Player[] players;
    public Player[] Players { get { return players; } }
    #endregion

    // Data (Stat)
    protected PlayerData pData;
    public PlayerData PData { get { if (pData == null) pData = GameManager.Instance.PlayerData_Manager.PData;  return pData; } }
    
    // HP 
    private float curHp;
    public float CurHP { get { return curHp; } set { curHp = value;  GameManager.Instance.UI_Controller.HP.UpdateCurHpBar(curHp); } }
    private float maxHp;
    public float MaxHp { get { return maxHp; } set { maxHp = value; GameManager.Instance.UI_Controller.HP.UpdateMaxHPBar(maxHp,curHp); } }
    
    // Control & Detect
    public PlayerGroundCheck GroundChecker { get; set; }

    private bool canControlPlayer = true;
    public bool CanControlPlayer { get { return canControlPlayer; } set { canControlPlayer = value; players[(int)currentType].CanControll = value; } }
   
    #region Setting
    private void Awake()
    {
        InitPlayerInformation();
    }

    public void InitPlayerInformation()
    {
        ActiveType();

        int playerCnt = players.Length;
        for (int idx = 0; idx < playerCnt; idx++)
        {
            players[idx].Init(this);
        }
        
        GroundChecker = GetComponentInChildren<PlayerGroundCheck>();
        GroundChecker.Init(currentType);
    }

    public void InitHP()
    {
        MaxHp = PData.maxHP;
        CurHP = maxHp;
    }

    public void LoadHP()
    {
        CurHP = PData.curHP;
        MaxHp = PData.maxHP;
    }
    #endregion

    #region Player Control Manage
    private void Update()
    {
        if (canControlPlayer)
            InputChangeType();
        InputPlayer();
    }

    public void ActiveType()
    {
        switch (currentType)
        {
            case PlayerType.Normal:
                players[0].gameObject.SetActive(true);
                //players[1].gameObject.SetActive(false);
                //players[2].gameObject.SetActive(false);
                break;
            default:
                break;
        }
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
    #endregion

    #region Heal & Hit
    public void Heal(float _healValue)
    {
        healParticle.Play();
        if (_healValue + curHp > maxHp)
            CurHP = MaxHp;
        else
            CurHP += _healValue;
    }

    public void Hit(float _damage)
    {
        players[(int)currentType].Hit(_damage);
    }
    #endregion
}
