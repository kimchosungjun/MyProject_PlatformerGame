using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossAttackType
{
     Near,
     Far,
     None
}

public class BossAttackTrigger : MonoBehaviour
{
    public BossAttackType attackType = BossAttackType.None;
    PlayerController pController = null;
    public PlayerController PController { get { if (pController == null) pController = GameManager.Instance.Controller; return pController; } }
    public BoxCollider2D triggerCollider;

    BossEnemy bossEnemy;
    BossStat stat;
    public BossStat Stat { get { if (stat == null) { GetStat(); } return stat; } }
    bool isCollide = false;

    public void GetStat()
    {
        bossEnemy = GetComponentInParent<BossEnemy>();
        stat = bossEnemy.bossStat;
    }

    public void Setting(BossEnemy _bossEnemy)
    {
        if (bossEnemy == null)
        {
            bossEnemy = _bossEnemy;
            stat = bossEnemy.bossStat;
        }
        isCollide = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollide)
        {
            isCollide = true;
            switch (attackType)
            {
                case BossAttackType.Near:
                    PController.Hit(Stat.nearDamage);
                    isCollide = false;
                    break;
                case BossAttackType.Far:
                    PController.Hit(Stat.farDamage);
                    break;
            }
        }
    }

    public void TriggerActive()
    {
        triggerCollider.enabled = true;
    }

    public void TriggerInactive()
    {
        triggerCollider.enabled = false;
    }

    public void ObjectInActive()
    {
        gameObject.SetActive(false);
    }
}
