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
        }
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
            GameObject go = PoolManager.Instace.GetObjectPool(data.attackFabName);
            if(go==null)
                return;
            go.transform.position = attackTransform.position;
            NormalSkills normalSkills = go.GetComponent<NormalSkills>();
            normalSkills.InitAttackData(data, transform.localScale.x);
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
            buffSkillObject.SetActive(true);
            NormalSkills normalSkills = buffSkillObject.GetComponent<NormalSkills>();
            normalSkills.InitBuffData(data);
        }
    }

    public void AttackSkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!CanAttackSkill())
                return;
            GameObject go = PoolManager.Instace.GetObjectPool(data.attackSkillFabName);
            if (go == null)
                return;
            go.transform.position = attackSkillPosition.position;
            NormalSkills normalSkills = go.GetComponent<NormalSkills>();
            normalSkills.InitAttackData(data, transform.localScale.x);
        }
    }
    #endregion

    #region Collision Call Method

    public void HitByEnemy(Collider2D _collision)
    {
        rigid.velocity = Vector2.up*3f;
        StartCoroutine(HitByEnemyCor());
        Invinsibility(1f);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack") && !isInvincibility)
        {
            HitByEnemy(collision);
        }
    }
    #endregion
}
