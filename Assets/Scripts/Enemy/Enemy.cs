using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] EnemyData data;
    protected CurrentEnemyData curData;
    public CurrentEnemyData CurData { get { return curData; } set { curData = value; } }
   
    #region Bool Val
    [Header("Bool Value Check")]
    [SerializeField] protected bool isFrontGround = false;
    public bool IsFrontGroud { get { return isFrontDownGround; } set{ isFrontGround = value; } }

    [SerializeField] protected bool isFrontDownGround = false;
    public bool IsFrontDownGround { get { return isFrontDownGround; } set { isFrontDownGround = value; } }

    [SerializeField] protected bool isGround = false;
    public bool IsGround { get { return isGround; } set { isGround = value; } }

    [SerializeField] protected bool isNearPlayer = false;
    public bool IsNearPlayer { get { return isNearPlayer; } set { isNearPlayer = value; } }

    [SerializeField] protected bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    [SerializeField] protected bool canAttack = true;
    public bool CanAttack { get { return canAttack; } set { canAttack = value; } }

    [SerializeField] protected bool isAttack = false;
    
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

    public virtual void Hit(float _damage)
    {
        float trueDamage = _damage - curData.defence;
        if (trueDamage <= 0)
            trueDamage = 1;
        curData.hp -= trueDamage;
        if (curData.hp <= 0)
            anim.SetBool("Dead", true);
    }

    public virtual void Death()
    {
        Invoke("InvokeDeath", 0.5f);
    }

    public void InvokeDeath()
    {
        gameObject.SetActive(false);
        anim.SetBool("Dead", false);
    }
}
