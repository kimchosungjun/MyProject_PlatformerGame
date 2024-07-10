using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSkills : MonoBehaviour
{
    [SerializeField] SkillType skillType;
    [SerializeField] float attackSpeed;
    //[SerializeField] float attackSkillSpeed;
    Rigidbody2D rigid;
    PlayerData data;

    bool isRight;
    float timer = 0f;
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
        timer = 0f;
        data = _data;
        defaultAttackValue = _data.attackDamageValue;
        _data.attackDamageValue = data.buffSkillValue * defaultAttackValue;
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
                ProjectileAttack();
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ConvertEnum.ConvertEnumToString(PaltformType.Ground)) ||
            collision.CompareTag(ConvertEnum.ConvertEnumToString(PaltformType.JumpPlatform)))
        {
            this.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Enemy") && skillType==SkillType.Attack)
        {
            // 데미지 주기
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hit(attackDamage);
            switch (skillType)
            {
                case SkillType.Attack:
                    this.gameObject.SetActive(false);
                    break;
                case SkillType.AttackSkill:
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
