using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    #region Player Data
    [Header("Data")]
    [SerializeField] protected PlayerActionType currentActionType = PlayerActionType.Idle;
    [SerializeField] protected PlayerData data;
    public PlayerData Data { get { return data; } }
    #endregion

    #region Bool Val
    protected bool isGround;
    public bool IsGround { get { return isGround; } set { isGround = value; } }

    protected bool canRoll = true;
    public bool CanRoll { get { return canRoll; } }

    protected bool isRoll;
    public bool IsRoll { get { return isRoll; } set { isRoll = value; } }

    protected bool isInvincibility = false;
    public bool IsInvincibility { get { return isInvincibility; }}

    protected bool canControll =true;
    public bool CanControll { get { return canControll; } set { canControll = value; } }
    #endregion

    #region Component
    [Header("Component")]
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

    #region Common Value
    protected WaitForSeconds hitTimer = new WaitForSeconds(1f);
    protected Transform attackTransform;
    protected float horizontal;

    // Normal Attack, Skill CoolTime Check
    protected bool isCoolDownAttack = true;
    protected bool isCoolDownBuffSkill= true;
    protected bool isCoolDownAttackSkill = true;
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

    #region Timer (공격, 버프스킬, 공격스킬)
    public bool CanAttack()
    {
        if (isCoolDownAttack)
        {
            data.curMana -= data.attackDecreaseMana;
            if (data.curMana <= 0)
                controller.ChangeType(PlayerType.Normal);
            isCoolDownAttack = false;
            Invoke("AttackCoolTime", data.attackCoolTime);
            return true;
        }
        else
            return false;
    }

    public void AttackCoolTime()
    {
        isCoolDownAttack = true;
    }

    public bool CanBuffSkill()
    {
        if (isCoolDownBuffSkill)
        {
            int curMana = data.curMana - data.buffSkillDecreaseMana;
            if (curMana < 0)
                return false;
            data.curMana -= data.buffSkillDecreaseMana;
            if(data.curMana<=0)
                controller.ChangeType(PlayerType.Normal);
            isCoolDownBuffSkill = false;
            Invoke("BuffSkillCoolTime", data.buffSkillCoolTime);
            return true;
        }
        else
            return false;
    }

    public void BuffSkillCoolTime()
    {
        isCoolDownBuffSkill = true;
    }

    public bool CanAttackSkill()
    {
        if (isCoolDownAttackSkill)
        {
            int curMana = data.curMana - data.attackSkillDecreaseMana;
            if (curMana < 0)
                return false;
            data.curMana -= data.attackSkillDecreaseMana;
            if (data.curMana <= 0)
                controller.ChangeType(PlayerType.Normal);
            isCoolDownAttackSkill = false;
            Invoke("AttackSkillCoolTime", data.attackSkillCoolTime);
            return true;
        }
        else
            return false;
    }

    public void AttackSkillCoolTime()
    {
        isCoolDownAttackSkill = true;
    }

    #endregion

    #region Hitting
    public virtual void Hit(float _damamge)
    {
        if (isInvincibility)
            return;
        rigid.velocity = Vector2.up * 3f;
        StartCoroutine(HitByEnemyCor());
        Invinsibility(1f);

        float trueDamage = _damamge-data.defenceValue;
        if (trueDamage <= 0)
            trueDamage = 1f;
        controller.CurHP -= _damamge;
        if (controller.CurHP <= 0)
        {
            canControll = false;
            anim.SetBool("Death", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
            gameObject.tag = "Death";
            // Death UI
        }
        else
        {
            anim.SetTrigger("Hurt");
        }
    }

    public IEnumerator HitByEnemyCor()
    {
        Color color = sprite.color;
        color.a = 0.5f;
        sprite.color = color;
        yield return hitTimer;
        color.a = 1f;
        sprite.color = color;
    }

    #endregion
}
