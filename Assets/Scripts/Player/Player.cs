using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected PlayerActionType currentActionType = PlayerActionType.Idle;
    [SerializeField] PlayerData data;
    public PlayerData Data { get { return data; } }
    protected Transform attackTransform;

    #region Bool Val
    protected bool isGround;
    public bool IsGround { get { return isGround; } set { isGround = value; } }

    protected bool canRoll = true;
    public bool CanRoll { get { return canRoll; } }

    private bool isRoll;
    public bool IsRoll { get { return isRoll; } set { isRoll = value; } }

    protected bool isInvincibility = false;
    public bool IsInvincibility { get { return isInvincibility; }}
    protected bool canControll =true;
    public bool CanControll { get { return canControll; } set { canControll = value; } }
    #endregion

    #region Component
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected SpriteRenderer sprite;
    protected PlayerController controller;
    protected PlayerState[] playerStates;
    protected PlayerStateMachine stateMachine;

    public Animator Anim { get { return anim; } }
    public Rigidbody2D Rigid { get { return rigid; } }
    public PlayerController Controller { get { return controller; } }
    public PlayerState[] PlayerStates { get { return playerStates; } }
    public PlayerStateMachine StateMachine { get { return stateMachine; } }
    #endregion

    #region Unity Life Cycle (Init : Awake, Execute : Update)
    public abstract void Init(PlayerController _playerController); // Awake
    public abstract void Execute(); // Update
    #endregion

    public void ChangeActionState(PlayerActionType _actionType)
    {
        currentActionType = _actionType;
        stateMachine.ChangeState(playerStates[(int)currentActionType]);
    }

    #region Timer (무적, 구르기)
    public void Invinsibility(float _timer)
    {
        StartCoroutine(InvincibilityCoolTimeCor(_timer));
    }

    public IEnumerator InvincibilityCoolTimeCor(float _timer)
    {
        isInvincibility = true;
        yield return new WaitForSeconds(_timer);
        isInvincibility = false;
    }

    public void Roll()
    {
        StartCoroutine(RollCoolTimerCor(data.rollCoolTime));
    }

    public IEnumerator RollCoolTimerCor(float _timer)
    {
        canRoll = false;
        yield return new WaitForSeconds(_timer);
        canRoll = true;
    }
    #endregion
}
