using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetect : MonoBehaviour
{
    bool isFindPlayer = false;

    public bool IsDetectPlayer()
    {
        return isFindPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFindPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFindPlayer = false;
        }
    }
}
