using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneTrigger : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] GameObject mapBounds;
    [SerializeField] Vector3 movePos;
    Vector3 currentPos;

    bool onceCall = true;

    private void Awake()
    {
        mainCam = Camera.main;  
    }

    public void CameraEffect()
    {
        StartCoroutine(CameraMoveCor());
    }

    public IEnumerator CameraMoveCor()
    {
        mapBounds.SetActive(false);
        float timer = 0f;
        currentPos = mainCam.transform.position;
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            mainCam.transform.position = Vector3.Lerp(currentPos, movePos, timer / 3f);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        timer = 0f;
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            mainCam.transform.position = Vector3.Lerp(movePos, currentPos, timer / 3f);
            yield return null;
        }
        mapBounds.SetActive(true);
        GameManager.Instance.Controller.CanControlPlayer = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && onceCall)
        {
            GameManager.Instance.Controller.CanControlPlayer = false;
            onceCall = false;
            CameraEffect();
        }
    }
}
