using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : Enemy
{
    float timer = 0f;
    float lookDirX = 1f;
    public float LookDirX { get { return lookDirX; } set { lookDirX = value; FlipX(value); } }
    EnemyActionType currentType = EnemyActionType.Idle;

    [Header("Bow Enemy Information")]
    [SerializeField] Transform magicPosition;
    [SerializeField, Range(0,15f)] float attackRange = 15f;
    [SerializeField, Tooltip("find behaviour timer")]float findTimer = 3f;
    [SerializeField, Tooltip("idle, move behaviour timer")]float notFindTimer = 3f; 

    private void Update()
    {
        Execute();
        AnimationControl();
    }

    public void AnimationControl()
    {
        if (rigid.velocity.x == 0)
            anim.SetBool("Move", false);
        else
            anim.SetBool("Move", true);
        if (rigid.velocity.y < 0)
            anim.SetBool("Fall", true);
        else
            anim.SetBool("Fall", false);
    }

    public void Execute()
    {
        switch (currentType)
        {
            case EnemyActionType.Idle:
                IdleAI();
                break;
            case EnemyActionType.Move:
                MoveAI();
                break;
            case EnemyActionType.Find:
                FindAI();
                break;
        }
    }

    public void IdleAI()
    {
        if (!canMove)
            return;

        if (isNearPlayer)
        {
            currentType = EnemyActionType.Find;
            timer = 0f;
            return;
        }

        rigid.velocity = new Vector2(0, rigid.velocity.y);

        timer += Time.deltaTime;
        if (timer > notFindTimer)
        {
            int randNum = Random.Range(0, 2);
            timer = 0f;
            if (randNum != 0)
            {
                randNum = Random.Range(0, 2);
                if (randNum == 0)
                    LookDirX = 1f;
                else
                    LookDirX = -1f;
                currentType = EnemyActionType.Move;
                return;
            }
        }
    }

    public void MoveAI()
    {
        if (!canMove)
        {
            currentType = EnemyActionType.Idle;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            return;
        }

        if (isFrontGround || !isFrontDownGround)
        {
            LookDirX *= -1f;
            timer = 0f;
            rigid.velocity = new Vector2(lookDirX * curData.moveSpeed, rigid.velocity.y);
            return;
        }
        rigid.velocity = new Vector2(lookDirX * curData.moveSpeed, rigid.velocity.y);

        if (isNearPlayer)
        {
            currentType = EnemyActionType.Find;
            //timer = 0f;
            return;
        }

        timer += Time.deltaTime;
        if (timer > notFindTimer)
        {
            int randNum = Random.Range(0, 2);
            timer = 0f;
            if (randNum != 0)
            {
                currentType = EnemyActionType.Idle;
                return;
            }
            else
            {
                randNum = Random.Range(0, 2);
                if (randNum == 0)
                    LookDirX = 1f;
                else
                    LookDirX = -1f;
            }
        }
    }

    public void FindAI()
    {
        if (canMove)
        {
            if(!isFrontGround)
                rigid.velocity = new Vector2(lookDirX * curData.moveSpeed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            return;
        }

        if (controller == null)
            controller = GameManager.Instance.Controller;
        if (controller.transform.position.x < transform.position.x) // 플레이어가 왼쪽에 있음
            LookDirX = -1f;
        else if (controller.transform.position.x > transform.position.x)
            LookDirX = 1f;

        if (isNearPlayer)
        {
            timer = 0f;
           
            float Distance = Vector2.Distance(controller.transform.position, transform.position);
            if (Distance < attackRange )
            {
                if (!canMove)
                    return;
                // Attack : 땅에 닿아있을때만
                if (CanAttack)
                {
                    if (isGround)
                    {
                        canAttack = false;
                        anim.SetTrigger("Attack");
                    }
                }
                else
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }
            else
            {
                // Follow Player
                rigid.velocity = new Vector2(lookDirX * curData.moveSpeed, rigid.velocity.y);
            }
        }
        else // 주변에 플레이어가 없을 때
        {
            timer += Time.deltaTime;
            if (timer > findTimer)
            {
                int randNum = Random.Range(0, 2);
                timer = 0f;
                if(randNum==0)
                {
                    currentType = EnemyActionType.Idle;
                    return;
                }
                else
                {
                    currentType = EnemyActionType.Move;
                    return;
                }
            }
        }
    }

    public void FlipX(float _dir)
    {
        if (_dir >= 1)
            transform.localScale = new Vector3(4, 4, 1);
        else if (_dir <= -1)
            transform.localScale = new Vector3(-4, 4, 1);
    }

    public void MagicFire()
    {
        GameObject go = PoolManager.Instace.GetObjectPool("MagicFire");
        go.transform.position = magicPosition.position;
        go.GetComponent<EnemyProjectile>().SetDir((Vector2)(controller.transform.position - transform.position), curData);
        StartCoroutine(AttackCoolDownCor());
    }

    public IEnumerator AttackCoolDownCor()
    {
        yield return new WaitForSeconds(curData.attackCoolTime);
        canAttack = true;
    }
}
