using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSkills : MonoBehaviour
{
    [SerializeField] SkillType skillType;
    
    [Header("기본공격")]
    [SerializeField] float attackSpeed;
    
    [Header("공격스킬")]
    [SerializeField] GameObject attackSkillParObj;
       
    Rigidbody2D rigid;
    PlayerData data;

    bool isRight;
    [SerializeField]float timer = 0f;
    float attackDamage;
    float defaultAttackValue;

    public void InitAttackData(PlayerData _data, float _dir)
    {
        rigid = GetComponent<Rigidbody2D>();
        data = _data;
        if (_dir <= -1)
            isRight = false;
        else if (_dir >= 1)
            isRight = true;
        attackDamage = _data.attackDamageValue;
    }

    public void InitBuffData(PlayerData _data)
    {
        data = _data;
        defaultAttackValue = _data.attackDamageValue;
        _data.attackDamageValue = data.buffSkillValue * defaultAttackValue;
        GameManager.Instance.UI_Controller.Enhance.IsUseBuff = true;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        switch (skillType)
        {
            case SkillType.Attack:
                ProjectileAttack();
                break;
            case SkillType.Buff:
                Buff();
                break;
            case SkillType.AttackSkill:
                AttackSkill();
                break;
        }        
    }
    public void ProjectileAttack()
    {
        if (isRight)
            rigid.velocity = new Vector2(attackSpeed, 0);
        else
            rigid.velocity = new Vector2(-attackSpeed, 0);
    }

    public void Buff()
    {
        timer += Time.deltaTime;
        if (timer >= data.buffMaintainTime)
        {
            data.attackDamageValue = defaultAttackValue;
            this.gameObject.SetActive(false);
            GameManager.Instance.UI_Controller.Enhance.IsUseBuff = false;
        }
    }

    public void AttackSkill()
    {
        if (isRight)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        attackSkillParObj.SetActive(true);
        timer += Time.deltaTime;
        if (timer >=2f)
        {
            attackSkillParObj.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ConvertEnum.ConvertEnumToString(PaltformType.Ground)) ||
            collision.CompareTag(ConvertEnum.ConvertEnumToString(PaltformType.JumpPlatform)))
        {
            if (skillType == SkillType.Attack)
            {
                this.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            // 데미지 주기
            Enemy enemy = collision.GetComponent<Enemy>();
            switch (skillType)
            {
                case SkillType.Attack:
                    this.gameObject.SetActive(false);
                    enemy.Hit(attackDamage);
                    break;
                case SkillType.AttackSkill:
                    enemy.Hit(attackDamage * 2);
                    break;
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(skillType != SkillType.Buff)
            this.gameObject.SetActive(false);
    }
}
