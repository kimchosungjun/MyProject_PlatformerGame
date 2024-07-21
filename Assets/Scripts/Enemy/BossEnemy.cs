using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossStat
{
    public float farDamage;
    public float nearDamage;
    public float moveSpeed;
    public float defenceValue;
    public float maxHP;
}

public class BossEnemy : Enemy
{
    public BossStat bossStat = new BossStat();
    public float currentHP;
    public float CurrentHP { get { return currentHP; }  set { currentHP = value; } }

    #region Setting
    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hitColor = sprite.color;
    }

    protected override void OnEnable()
    {
        if (bossStat != null)
        {
            isDeath = false;
            CurrentHP = bossStat.maxHP;            
        }
    }

    public void Start()
    {
        GameManager.Instance.UI_Controller.BossHP.Init(bossStat);
        GameManager.Instance.UI_Controller.BossHP.OnOffUI(true);
    }

    public override void Hit(float _damage)
    {
        float trueDamage = _damage - bossStat.defenceValue;
        if (trueDamage <= 0)
            trueDamage = 1;
        CurrentHP -= trueDamage;
        StartCoroutine(HitCor());
        if (CurrentHP <= 0)
        {
            isDeath = true;
            anim.SetBool("Death", true);
            gameObject.tag = "Death";
            // Boss Ending Trigger ¹ß»ý
            GameManager.Instance.UI_Controller.BossHP.OnOffUI(false);
        }
    }
    #endregion

    #region AI

    private void Update()
    {
        
    }

    public void AttackAI()
    {

    }


    public void PhaseOne()
    {

    }
    #endregion
}
