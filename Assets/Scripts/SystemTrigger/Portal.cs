using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField, Tooltip("World Position")] Vector2 nextVec;
    [SerializeField] string nextSceneName;
    PlayerController controller;
    bool canUsePortal = false;
    public enum PortalType
    {
        Battle,
        NextBattle,
        Town,
        Boss
    }
    [SerializeField, Tooltip("다음으로 이동하는 곳")] PortalType portalType = PortalType.Town;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canUsePortal)
            {
                UsePortal();
            }
        }
    }

    public void UsePortal()
    {
        switch (portalType)
        {
            case PortalType.Battle:
                GameManager.LoadScene_Manager.LoadScene(nextSceneName);
                break;
            case PortalType.NextBattle:
                controller.gameObject.transform.position = nextVec;
                break;
            case PortalType.Town:
                GameManager.LoadScene_Manager.LoadScene(nextSceneName);
                break;
            case PortalType.Boss:
                GameManager.LoadScene_Manager.LoadScene(nextSceneName);
                break;
        }
        GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, transform);
        Destroy(transform.parent.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(controller==null)
            {
                controller = collision.GetComponent<PlayerController>();
            }
            canUsePortal = true;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(true, transform);
            // 인디케이터 활성화
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canUsePortal = false;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, transform);
            // 인디케이터 비활성화
        }
    }
}
