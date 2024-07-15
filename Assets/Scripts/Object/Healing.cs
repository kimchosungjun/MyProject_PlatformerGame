using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] float healValue;
    [SerializeField] float healTime;
    bool isCollidePlayer = false;
    PlayerController controller = null;
    WaitForSeconds healWaitTime;

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

        controller.Heal(healValue);
        yield return healWaitTime;
        StartCoroutine(HealCor());
    }
}
