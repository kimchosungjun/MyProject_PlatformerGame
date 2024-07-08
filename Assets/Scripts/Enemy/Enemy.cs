using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 데이터")]
    [SerializeField] EnemyData data;
    protected CurrentEnemyData curData;
    public CurrentEnemyData CurData { get { return curData; } set { curData = value; } }
    #region Bool Val
    protected bool isFrontGround = false;
    public bool IsFrontGround { get { return isFrontGround; } set { isFrontGround = value; } }

    protected bool isGround = false;
    public bool IsGround { get { return isGround; } set { isGround = value; } }

    protected bool isNearPlayer = false;
    public bool IsNearPlayer { get { return isNearPlayer; } set { isNearPlayer = value; } }

    protected bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    protected bool canAttack = true;
    public bool CanAttack { get { return canAttack; } set { canAttack = value; } }
    #endregion

    #region Common Component
    protected Rigidbody2D rigid;
    public Rigidbody2D Rigid { get { return rigid; } }
    
    protected Animator anim;
    public Animator Anim { get { return anim; } }
    
    protected PlayerController controller;
    public PlayerController Controller { get { return controller; } }
    #endregion

    protected virtual void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curData = new CurrentEnemyData(data);
    }

    protected void OnEnable()
    {
        if (curData != null)
            curData.hp = curData.maxHp;
    }

    public void Hit()
    {

    }
}
