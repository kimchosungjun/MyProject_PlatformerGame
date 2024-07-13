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

    protected bool isRoll;
    public bool IsRoll { get { return isRoll; } set { isRoll = value; } }

    protected bool isInvincibility = false;
    public bool IsInvincibility { get { return isInvincibility; } }

    protected bool canControll = true;
    public bool CanControll { get { return canControll; } set { canControll = value;  } }
    #endregion

    #region Component
    [Header("Component")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected SpriteRenderer sprite;
    protected PlayerController controller;
    protected PlayerState[] playerStates;
    protected PlayerStateMachine stateMachine;

    protected UIController ui_Controller;
    public UIController UI_Controller { get { if (ui_Controller == null) ui_Controller = GameManager.Instance.UI_Controller; return ui_Controller; } }

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
    protected bool isCoolDownRoll = true;
    public bool IsCoolDownRoll { get { return isCoolDownRoll; } }
    protected bool isCoolDownAttack = true;
    protected bool isCoolDownBuffSkill = true;
    protected bool isCoolDownAttackSkill = true;

    // Press Key Cool Timer 
    protected float rollCoolTimer = 0f;
    protected float attackCoolTimer = 0f;
    protected float buffSkillCoolTimer = 0f;
    protected float attackSkillCoolTimer = 0f;

    public float RollCoolTimer { get { return rollCoolTimer; } }
    public float AttackCoolTimer { get { return attackCoolTimer; } }
    public float BuffSkillCoolTimer { get { return buffSkillCoolTimer; } }
    public float AttackSkillCoolTimer { get { return attackSkillCoolTimer; } }
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

    #region Timer (무적)
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
    #endregion

    #region Timer (구르기, 공격, 버프스킬, 공격스킬)
    public void CoolTimerChecker()
    {
        // Roll
        if (!isCoolDownRoll)
        {
            rollCoolTimer += Time.deltaTime;
            if (rollCoolTimer > data.rollCoolTime)
                isCoolDownRoll = true;
        }
        // Attack
        if (!isCoolDownAttack)
        {
            attackCoolTimer += Time.deltaTime;
            if (attackCoolTimer > data.attackCoolTime)
                isCoolDownAttack = true;
        }
        // Buff
        if (!isCoolDownBuffSkill)
        {
            buffSkillCoolTimer += Time.deltaTime;
            if (buffSkillCoolTimer > data.buffSkillCoolTime)
                isCoolDownBuffSkill = true;
        }
        // Attack Skill
        if (!isCoolDownAttackSkill)
        {
            attackSkillCoolTimer += Time.deltaTime;
            if (attackSkillCoolTimer > data.attackSkillCoolTime)
                isCoolDownAttackSkill = true;
        }
    }

    public bool CanRoll()
    {
        if (isCoolDownRoll)
        {
            isCoolDownRoll = false;
            rollCoolTimer = 0f;
            UI_Controller.Key.KeyPress(data.rollCoolTime, PressKeyType.Roll);
            return true;
        }
        return false;
    }

    public bool CanAttack()
    {
        if (isCoolDownAttack)
        {
            data.curMana -= data.attackDecreaseMana;
            if (data.curMana <= 0 && controller.CurrentType!=PlayerType.Normal)
                controller.ChangeType(PlayerType.Normal);
            isCoolDownAttack = false;
            attackCoolTimer = 0f;
            UI_Controller.Key.KeyPress(data.attackCoolTime, PressKeyType.Attack);
            return true;
        }
        else
            return false;
    }

    public bool CanBuffSkill()
    {
        if (isCoolDownBuffSkill)
        {
            int curMana = data.curMana - data.buffSkillDecreaseMana;
            if (curMana < 0)
                return false;
            data.curMana -= data.buffSkillDecreaseMana;
            if(data.curMana<=0 && controller.CurrentType != PlayerType.Normal)
                controller.ChangeType(PlayerType.Normal);
            isCoolDownBuffSkill = false;
            buffSkillCoolTimer = 0f;
            UI_Controller.Key.KeyPress(data.buffSkillCoolTime, PressKeyType.Buff);
            return true;
        }
        else
            return false;
    }

    public bool CanAttackSkill()
    {
        if (isCoolDownAttackSkill)
        {
            int curMana = data.curMana - data.attackSkillDecreaseMana;
            if (curMana < 0)
                return false;
            data.curMana -= data.attackSkillDecreaseMana;
            if (data.curMana <= 0 && controller.CurrentType != PlayerType.Normal)
                controller.ChangeType(PlayerType.Normal);
            isCoolDownAttackSkill = false;
            attackSkillCoolTimer = 0f;
            UI_Controller.Key.KeyPress(data.attackSkillCoolTime, PressKeyType.AttackSkill);
            return true;
        }
        else
            return false;
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
