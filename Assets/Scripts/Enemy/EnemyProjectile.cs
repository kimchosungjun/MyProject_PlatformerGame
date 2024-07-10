using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator anim;
    CurrentEnemyData data;

    public void SetDir(Vector2 _dir, CurrentEnemyData _data)
    {
        Vector2 dir = _dir.normalized;
        rigid.velocity = dir * moveSpeed;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigid.rotation = angle;
        data = _data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            controller.Hit(data.damage);
            rigid.velocity = Vector2.zero;
            anim.Play("MagicFireEx");
        }

        if(collision.CompareTag("Ground") || collision.CompareTag("JumpPlatform"))
        {
            gameObject.SetActive(false);
        }
    }

    public void OffThis()
    {
        gameObject.SetActive(false);
    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
