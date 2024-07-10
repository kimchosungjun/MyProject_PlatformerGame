using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] EnemyData data;
    protected Color hitColor = new Color();
    protected SpriteRenderer sprite;
    protected CurrentEnemyData curData;
    protected WaitForSeconds hitTime = new WaitForSeconds(0.2f);
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
        sprite = GetComponent<SpriteRenderer>();
        curData = new CurrentEnemyData(data);
        hitColor = sprite.color;
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
        StartCoroutine(HitCor());
        if (curData.hp <= 0)
        {
            anim.SetBool("Death", true);
            gameObject.tag = "Death";
            canMove = false;
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
        anim.SetBool("Death", false);
        gameObject.tag = "Enemy";
        canMove = true;
    }

    public IEnumerator HitCor()
    {
        hitColor.a = 0.5f;
        sprite.color = hitColor;
        yield return hitTime;
        hitColor.a = 1f;
        sprite.color = hitColor;
    }
}
