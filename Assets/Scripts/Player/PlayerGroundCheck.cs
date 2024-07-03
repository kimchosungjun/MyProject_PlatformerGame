using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField, Tooltip("0:노말, 1: 바람, 2: 물")] Player[] players;
    public PlayerType CurrentType { get; set; } = PlayerType.Normal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            players[(int)CurrentType].IsGround = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            players[(int)CurrentType].IsGround = false;
        }
    }
}
