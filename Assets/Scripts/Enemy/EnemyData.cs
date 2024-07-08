using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName ="Enemy/EnemyData",order =1)]
public class EnemyData : ScriptableObject
{
    [Header("�� ��ġ���� �ҷ����� ��. ����ؼ� �ȵ�.")]
    public float hp;
    public float maxHp;
    public float damage;
    public float defence;
    public float moveSpeed;
    public float attackCoolTime;
}

public class CurrentEnemyData
{
    public float hp;
    public float maxHp;
    public float damage;
    public float defence;
    public float moveSpeed;
    public float attackCoolTime; 

    public CurrentEnemyData(EnemyData _data)
    {
        hp = _data.hp;
        maxHp = _data.maxHp;
        damage = _data.damage;
        defence = _data.moveSpeed;
        moveSpeed = _data.moveSpeed;
        attackCoolTime = _data.attackCoolTime;
    }
}
