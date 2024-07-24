using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNormalStateSpace;

public class NormalPlayer : Player
{
    [SerializeField] GameObject buffSkillObject;
    [SerializeField] Transform attackSkillPosition;
    #region Life Cycle
    public override void Init(PlayerController _playerController) // = Awake
    {
        if (soundPlayer == null)
            soundPlayer = GetComponentInChildren<SFXSoundPlayer>();
        controller = _playerController;
        stateMachine = new PlayerStateMachine();
        playerStates = new PlayerState[(int)PlayerActionType.Roll + 1];
        playerStates[(int)PlayerActionType.Idle] = new NormalIdle(this);
        playerStates[(int)PlayerActionType.Move] = new NormalMove(this);
        playerStates[(int)PlayerActionType.Jump] = new NormalJump(this);
        playerStates[(int)PlayerActionType.Fall] = new NormalFall(this);
        playerStates[(int)PlayerActionType.Roll] = new NormalRoll(this);
        attackTransform = controller.AttackTransform;
        stateMachine.ChangeState(playerStates[(int)currentActionType]);
    }

    public override void Execute() // = Update
    {
        if (canControll)
        {
            stateMachine.Execute();
            Flip();
            Attack();
            BuffSkill();
            AttackSkill();
        }
        else
        {
            horizontal = 0;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if (!IsGround)
            {
                if (rigid.velocity.y == 0)
                    anim.SetBool("Fall", false);
                if (rigid.velocity.y < 0)
                    anim.SetBool("Jump", false);
            }
            anim.SetBool("Move", false);
            anim.SetBool("Roll", false);
            anim.SetBool("Idle", true);

        }
        CoolTimerChecker();
    }
    #endregion

    #region Key Call Method
    public void Flip()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!CanAttack())
                return;
            anim.SetTrigger("Attack");
            GameObject go = PoolManager.Instace.GetObjectPool("NoramlAttack");
            if (go == null)
                return;
            soundPlayer.PlayPlayerSFX(PlayerSoundType.Attack);
            go.transform.position = attackTransform.position;
            NormalSkills normalSkills = go.GetComponent<NormalSkills>();
            normalSkills.InitAttackData(PData, transform.localScale.x);
        }
    }

    public void BuffSkill()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!CanBuffSkill())
                return;
            if (buffSkillObject == null)
                return;
            soundPlayer.PlayPlayerSFX(PlayerSoundType.Buff);
            buffSkillObject.SetActive(true);
            NormalSkills normalSkills = buffSkillObject.GetComponent<NormalSkills>();
            normalSkills.InitBuffData(pData);
        }
    }

    public void AttackSkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!CanAttackSkill())
                return;
            anim.SetTrigger("Attack");
            GameObject go = PoolManager.Instace.GetObjectPool("NormalAttackSkill");
            if (go == null)
                return;
            soundPlayer.PlayPlayerSFX(PlayerSoundType.AttackSkill);
            go.transform.position = attackSkillPosition.position;
            NormalSkills normalSkills = go.GetComponent<NormalSkills>();
            normalSkills.InitAttackData(PData, transform.localScale.x);
        }
    }
    #endregion
}
