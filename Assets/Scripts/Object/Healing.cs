using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] float healTime;
    bool isCollidePlayer = false;
    PlayerController controller = null;
    WaitForSeconds healWaitTime;

    float healValue = -2;
    private void Awake()
    {
        healWaitTime = new WaitForSeconds(healTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (controller == null)
            {
                controller = collision.GetComponent<PlayerController>();
            }
            isCollidePlayer = true;
            StartCoroutine(HealCor());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollidePlayer = false;
        }
    }

    public IEnumerator HealCor()
    {
        if (!isCollidePlayer || controller==null)
            yield break;

        if (healValue <= -1)
            healValue = (controller.MaxHp - controller.CurHP) / 3;

        controller.Heal(healValue);
        yield return healWaitTime;
        StartCoroutine(HealCor());
    }
}
