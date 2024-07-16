using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [Header("땅 검출")]
    [SerializeField] protected Transform enemyTransform;
    [SerializeField] protected Transform cliffTranfrom;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected float frontDistance;

    [Header("땅 검출 Bool 변수")]
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool isCliffGround;
    [SerializeField] protected bool isFrontGround;

    [Header("플레이어 감지 변수")]
    [SerializeField] EnemyPlayerDetect playerDetect;
    [SerializeField] protected bool isNearPlayer = false;
    [SerializeField] protected bool isLookRight = true;

    [Header("AI 타이머 변수")]
    [SerializeField] protected float idleTimer = 0f;
    [SerializeField] protected float idleMaintainTime = 3f;
    [SerializeField] protected float moveTimer = 0f;
    [SerializeField] protected float moveMaintainTime = 3f;
    [SerializeField] protected float findTimer = 0f;
    [SerializeField] protected float findMaintainTime = 5f;
    [SerializeField] protected float attackTimer = 0f;

    [Header("AI 공격 변수")]
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected float attackRange = 5f;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool canAttack = true;
    protected override void Awake()
    {
        base.Awake();
        if (playerDetect == null)
            playerDetect = GetComponentInChildren<EnemyPlayerDetect>();
    }

    public void LayerChecker()
    {
        // Front Ground
        Vector2 detectVec;
        if (transform.localScale.x > 0)
            detectVec = Vector2.right;
        else
            detectVec = Vector2.left;

        if (Physics2D.Raycast(enemyTransform.position, detectVec, frontDistance, LayerMask.GetMask("Ground")))
            isFrontGround = true;
        else
            isFrontGround = false;

        // Down Ground
        if (Physics2D.Raycast(enemyTransform.position, Vector2.down, groundDistance, LayerMask.GetMask("Ground")))
            isGround = true;
        else
            isGround = false;

        // Cliff Ground
        if (Physics2D.Raycast(cliffTranfrom.position, Vector2.down, groundDistance, LayerMask.GetMask("Ground")))
            isCliffGround = true;
        else
            isCliffGround = false;

        // Near Player
        isNearPlayer = playerDetect.IsDetectPlayer();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyTransform.position, enemyTransform.position + Vector3.down * groundDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(cliffTranfrom.position, Vector3.down * groundDistance + new Vector3(cliffTranfrom.position.x, 0, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(enemyTransform.position, enemyTransform.position + transform.localScale.x * Vector3.right * frontDistance);
    }

    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("JumpPlatform"))
    //        Debug.Log(collision.collider.gameObject.layer);
    //}
}
