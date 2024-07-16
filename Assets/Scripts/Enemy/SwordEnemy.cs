using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : NormalEnemy
{
    [SerializeField] EnemyActionType currentType = EnemyActionType.Idle;
    [SerializeField] GameObject swordObject;
    [SerializeField] EnemySword enemySword;
    private void Start()
    {
        ChangeActionType(EnemyActionType.Move);
        if (playerController == null)
            playerController = GameManager.Instance.Controller;
        enemySword.SetInformation(curData);
    }

    public void Update()
    {
        LayerChecker();
        if (isDeath)
        {
            anim.SetBool("Move", false);
            anim.SetBool("Fall", false);
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
        else
        {
            AnimationChecker();
            Execute();
        }
    }

    public void AnimationChecker()
    {
        // Move
        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
            anim.SetBool("Move", false);
        else
            anim.SetBool("Move", true);
        // Fall
        if (isGround)
            anim.SetBool("Fall", false);
        else
            anim.SetBool("Fall", true);
    }

    public void Flip(bool _isRight)
    {
        if (!isGround)
            return;

        isLookRight = _isRight;
        if (_isRight)
            transform.localScale = new Vector3(2, 2, 2);
        else
            transform.localScale = new Vector3(-2, 2, 2);
    }
 
    public void Execute()
    {
        if (!canMove)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            return;
        }

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
        if(isNearPlayer)
        {
            ChangeActionType(EnemyActionType.Find);
            return;
        }

        // Idle AI
        idleTimer += Time.deltaTime;
        if (idleTimer > idleMaintainTime)
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                ChangeActionType(EnemyActionType.Idle);
                return;
            }
            else
            {
                ChangeActionType(EnemyActionType.Move);
                return;
            }
        }
    }

    public void MoveAI()
    {
        if (isFrontGround)
        {
            Flip(!isLookRight);
            return;
        }

        if (isNearPlayer)
        {
            ChangeActionType(EnemyActionType.Find);
            return;
        }

        //Move AI
        rigid.velocity = new Vector2(transform.localScale.x * curData.moveSpeed, rigid.velocity.y);
        moveTimer += Time.deltaTime;
        if (moveTimer > moveMaintainTime)
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                ChangeActionType(EnemyActionType.Idle);
                return;
            }
            else
            {
                ChangeActionType(EnemyActionType.Move);
                return;
            }
        }
    }

    public void FindAI()
    {
        if (playerController.transform.position.x - transform.position.x > 0.1f)
            Flip(true);
        else if (playerController.transform.position.x - transform.position.x < -0.1f)
            Flip(false); 

        if (isNearPlayer)
        {
            findTimer = 0f;
            float distance = Vector2.Distance(transform.position, playerController.transform.position);
            if (distance < attackRange)
            {
                if (canAttack)
                {
                    if (isFrontGround)
                        rigid.velocity = new Vector2(0, rigid.velocity.y);
                    canMove = false;
                    canAttack = false;
                    anim.SetTrigger("Attack");
                    Invoke("AttackTimer", curData.attackCoolTime);
                }
                else
                {
                    if (isFrontGround)
                        rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }
            else
            {
                if (isFrontGround)
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                else
                    rigid.velocity = new Vector2(curData.moveSpeed * transform.localScale.x, rigid.velocity.y);
            }
        }
        else
        {
            if (isFrontGround)
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(curData.moveSpeed * transform.localScale.x, rigid.velocity.y);
            // Set Another Type
            findTimer += Time.deltaTime;
            if (findTimer > findMaintainTime)
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 0)
                {
                    ChangeActionType(EnemyActionType.Idle);
                    return;
                }
                else
                {
                    ChangeActionType(EnemyActionType.Move);
                    return;
                }
            }
        }
    }

    public void ChangeActionType(EnemyActionType _changeType)
    {
        currentType = _changeType;
        switch (_changeType)
        {
            case EnemyActionType.Idle:
                idleTimer = 0f;
                break;
            case EnemyActionType.Move:
                moveTimer = 0f;
                int randNum = Random.Range(0, 2);
                if (randNum == 0)
                    Flip(true);
                else
                    Flip(false);
                break;
            case EnemyActionType.Find:
                findTimer = 0f;
                break;
        }
    }


    // Call By Attack Animation
    public void ActiveAttack()
    {
        swordObject.SetActive(true);
    }

    public void InActiveAttack()
    {
        canMove = true;
        swordObject.SetActive(false);
    }

    public void AttackTimer()
    {
        canAttack = true;
    }
}
