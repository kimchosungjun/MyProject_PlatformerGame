using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected SFXSoundPlayer soundPlayer;

    [Header("적 데이터")]
    [SerializeField] EnemyData data;
    protected Color hitColor = new Color();
    protected SpriteRenderer sprite;
    protected CurrentEnemyData curData;
    protected WaitForSeconds hitTime = new WaitForSeconds(0.2f);
    public CurrentEnemyData CurData { get { return curData; } set { curData = value; } }

    #region Bool Val
    [Header("제어 변수")]
    [SerializeField] protected bool isDeath = false;
    public bool IsDeath { get { return isDeath; } set { isDeath = value; } }
    #endregion

    #region Common Component
    protected Rigidbody2D rigid;
    public Rigidbody2D Rigid { get { return rigid; } }
    
    protected Animator anim;
    public Animator Anim { get { return anim; } }
    #endregion

    protected virtual void Awake()
    {
        if (soundPlayer == null)
            soundPlayer = GetComponentInChildren<SFXSoundPlayer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        curData = new CurrentEnemyData(data);
        hitColor = sprite.color;
    }

    protected virtual void OnEnable()
    {
        if (curData != null)
        {
            isDeath = false;
            curData.hp = curData.maxHp;
        }
    }

    #region Hit & Death
    public virtual void Hit(float _damage)
    {
        float trueDamage = _damage - curData.defence;
        if (trueDamage <= 0)
            trueDamage = 1;
        curData.hp -= trueDamage;
        StartCoroutine(HitCor());
        if (curData.hp <= 0)
        {
            isDeath = true;
            PoolManager.Instace.EnemyCnt -= 1;
            anim.SetBool("Death", true);
            gameObject.tag = "Death";
            soundPlayer.PlayEnemySFX(EnemySoundType.Death);
        }
        else
        {
            soundPlayer.PlayEnemySFX(EnemySoundType.Hit);
        }
    }

    public IEnumerator HitCor()
    {
        hitColor.a = 0.5f;
        sprite.color = hitColor;
        yield return hitTime;
        hitColor.a = 1f;
        sprite.color = hitColor;
    }

    public virtual void DoneDeath()
    {
        gameObject.SetActive(false);
        anim.SetBool("Death", false);
        gameObject.tag = "Enemy";
    }
    #endregion
}
