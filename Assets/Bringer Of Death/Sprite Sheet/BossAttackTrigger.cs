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

    BossStat stat;
    bool isCollide = false;


    public void Setting(BossStat _stat)
    {
        stat = _stat;
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
                    pController.Hit(stat.nearDamage);
                    break;
                case BossAttackType.Far:
                    pController.Hit(stat.farDamage);
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
