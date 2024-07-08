using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rigid;

    public void SetDir(Vector2 _dir)
    {
        Vector2 dir = _dir.normalized;
        rigid.velocity = dir * moveSpeed;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigid.rotation = angle;
    }
}
