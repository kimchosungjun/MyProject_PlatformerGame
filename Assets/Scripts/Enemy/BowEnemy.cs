using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : Enemy
{
    [SerializeField] EnemyActionType currentType = EnemyActionType.Idle;
    [SerializeField] Transform arrowPosition;
    [SerializeField] float attackRange = 5f;

    float timer = 0f;
    [SerializeField, Tooltip("find behaviour timer")]float findTimer = 5f;
    [SerializeField, Tooltip("idle, move behaviour timer")]float notFindTimer = 3f; 

    float lookDirX = 1f;
    public float LookDirX { get { return lookDirX; } set { lookDirX = value; FlipX(value); } }

    private void Update()
    {
        Execute();
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
        anim.SetInteger("XMove", 0);
        if (isNearPlayer)
        {
            currentType = EnemyActionType.Find;
            timer = 0f;
            return;
        }

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
        anim.SetInteger("XMove", 1);
        rigid.velocity = new Vector2(lookDirX * curData.moveSpeed, rigid.velocity.y);

        if (isNearPlayer)
        {
            currentType = EnemyActionType.Find;
            timer = 0f;
            return;
        }

        if (!isFrontGround)
        {
            LookDirX *=-1f;
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
        //if(rigid.velocity.y<0)
        //    anim.SetBool("Fall", true);
        //else
        //    anim.SetBool("Fall", false);
        anim.SetInteger("XMove", 1);

        if (canMove)
            rigid.velocity = new Vector2(lookDirX * curData.moveSpeed, rigid.velocity.y);
        else
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (isNearPlayer)
        {
            timer = 0f;
            if (controller == null)
                controller = GameManager.Instance.Controller;
            float Distance = Vector2.Distance(controller.transform.position, transform.position);
            if (Distance < attackRange)
            {
                // Attack : 땅에 닿아있을때만
                if (isGround && canMove && canAttack)
                {
                    canAttack = false;
                    canMove = false;
                    anim.SetTrigger("Attack");
                }
                else
                {
                    
                }
            }
            else
            {
                // Follow Player
                if (controller.transform.position.x < transform.position.x) // 플레이어가 왼쪽에 있음
                    LookDirX = -1f;
                else
                    LookDirX = 1f;
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

    public void ShootArrow()
    {
        GameObject go = PoolManager.Instace.GetObjectPool("Arrow");
        go.transform.position = arrowPosition.position;
        go.GetComponent<EnemyProjectile>().SetDir((Vector2)(controller.transform.position - transform.position));
    }

    public void DoneShootArrow()
    {
        canMove = true;
        StartCoroutine(AttackCoolDownCor());
    }

    public IEnumerator AttackCoolDownCor()
    {
        yield return new WaitForSeconds(curData.attackCoolTime);
        canAttack = true;
    }
}
