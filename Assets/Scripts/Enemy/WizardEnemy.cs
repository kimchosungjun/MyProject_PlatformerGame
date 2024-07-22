using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : NormalEnemy
{
    [Header("¸¶¹ý»ç")]
    [SerializeField] EnemyActionType currentType = EnemyActionType.Idle;
    [SerializeField] Transform magicPosition;

    private void Start()
    {
        ChangeActionType(EnemyActionType.Idle);
        if (playerController == null)
            playerController = GameManager.Instance.Controller;
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

    bool canFlip = true;
    [SerializeField] float flipTime = 0.1f;
    public void Flip(bool _isRight)
    {
        if (!isGround)
            return;

        if (!canFlip)
            return;

        isLookRight = _isRight;
        if (_isRight)
            transform.localScale = new Vector3(4, 4, 4);
        else
            transform.localScale = new Vector3(-4, 4, 4);
        canFlip = false;
        StartCoroutine(FlipTimerCor());
    }

    public IEnumerator FlipTimerCor()
    {
        yield return new WaitForSeconds(flipTime);
        canFlip = true;
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
        if (isNearPlayer)
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
        if (isFrontGround || !isCliffGround)
        {
            Flip(!isLookRight);
            rigid.velocity = new Vector2(transform.localScale.x * curData.moveSpeed, rigid.velocity.y);
            return;
        }

        if (isNearPlayer)
        {
            ChangeActionType(EnemyActionType.Find);
            return;
        }

        //Move AI
        rigid.velocity = new Vector2(transform.localScale.x * curData.moveSpeed , rigid.velocity.y);;
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
        if (isNearPlayer)
        {
            if (playerController.transform.position.x - transform.position.x > 0.1f)
                Flip(true);
            else if (playerController.transform.position.x - transform.position.x < -0.1f)
                Flip(false);

            findTimer = 0f;
            float distance = Vector2.Distance(transform.position, playerController.transform.position);
            if (distance < attackRange)
            {
                if (canAttack)
                {
                    if (isFrontGround || !isCliffGround )
                        rigid.velocity = new Vector2(0, rigid.velocity.y);
                    canMove = false;
                    canAttack = false;
                    anim.SetTrigger("Attack");
                    Invoke("AttackTimer", curData.attackCoolTime);
                }
                else
                {
                    if (isFrontGround || !isCliffGround )
                        rigid.velocity = new Vector2(0, rigid.velocity.y);
                    else
                        rigid.velocity = new Vector2(CurData.moveSpeed * (transform.localScale.x/Mathf.Abs(transform.localScale.x)), rigid.velocity.y);
                }
            }
            else
            {
                if (isFrontGround || !isCliffGround)
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                else
                    rigid.velocity = new Vector2(curData.moveSpeed * transform.localScale.x, rigid.velocity.y);
            }
        }
        else
        {
            if (isFrontGround || !isCliffGround)
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
        GameObject go = PoolManager.Instace.GetObjectPool("MagicFire");
        go.transform.position = magicPosition.position;
        go.GetComponent<EnemyProjectile>().SetDir((Vector2)(playerController.transform.position - transform.position), curData);
    }

    public void InActiveAttack()
    {
        canMove = true;
    }

    public void AttackTimer()
    {
        canAttack = true;
    }

}
