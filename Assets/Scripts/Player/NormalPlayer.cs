using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNormalStateSpace;

public class NormalPlayer : Player
{
    [SerializeField] float hitTimer = 0.2f;
    float horizontal;

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
            BuffSkil();
            AttackSkil();
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log($"{attackTransform.position.x},{attackTransform.position.y}에서 공격 발동!");
        }
    }

    public void BuffSkil()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("버프스킬 발동!");
        }
    }

    public void AttackSkil()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log($"{attackTransform.position.x},{attackTransform.position.y}에서 공격스킬 발동!");
        }
    }
    #endregion

    #region Collision Call Method

    public void HitByEnemy(Collision2D _collision)
    {
        if (_collision.transform.position.x > transform.position.x) // 오른쪽에서 때림
            rigid.AddForce(Vector2.up*3f, ForceMode2D.Impulse);
        else // 왼쪽에서 때림
            rigid.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        StartCoroutine(HitByEnemyCor());
        Invinsibility(hitTimer);
    }

    public IEnumerator HitByEnemyCor()
    {
        Color color = sprite.color;
        color.a = 0.5f;
        sprite.color = color;
        yield return new WaitForSeconds(hitTimer);
        color.a = 1f;
        sprite.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack") && !isInvincibility)
        {
            HitByEnemy(collision);
        }
    }
    #endregion
}
