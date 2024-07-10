using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    CurrentEnemyData data;
    PlayerController controller;
    public void SetInformation(CurrentEnemyData _data)
    {
        data = _data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (controller == null)
                controller = GameManager.Instance.Controller;
            controller.Hit(data.damage);
            gameObject.SetActive(false);
        }
    }
}
