using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerChecker : MonoBehaviour
{
    Enemy enemy;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public enum EnemyCheckType
    {
        IsGround,
        IsFrontGround,
        IsFrontDownGround,
        IsNearPlayer
    }

    [SerializeField] EnemyCheckType checkType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (checkType)
        {
            case EnemyCheckType.IsGround:
                if(collision.CompareTag("Ground"))
                    enemy.IsGround = true;
                break;
            case EnemyCheckType.IsFrontGround:
                if (collision.CompareTag("Ground")|| collision.CompareTag("JumpPlatform"))
                    enemy.IsFrontGroud = true;
                break;
            case EnemyCheckType.IsFrontDownGround:
                if (collision.CompareTag("Ground"))
                    enemy.IsFrontDownGround = true;
                break;
            case EnemyCheckType.IsNearPlayer:
                if (collision.CompareTag("Player"))
                    enemy.IsNearPlayer = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (checkType)
        {
            case EnemyCheckType.IsGround:
                if (collision.CompareTag("Ground"))
                    enemy.IsGround = false;
                break;
            case EnemyCheckType.IsFrontGround:
                if (collision.CompareTag("Ground") || collision.CompareTag("JumpPlatform"))
                    enemy.IsFrontGroud = false;
                break;
            case EnemyCheckType.IsFrontDownGround:
                if (collision.CompareTag("Ground"))
                    enemy.IsFrontDownGround = false;
                break;
            case EnemyCheckType.IsNearPlayer:
                if (collision.CompareTag("Player"))
                    enemy.IsNearPlayer = false;
                break;
        }
    }
}
