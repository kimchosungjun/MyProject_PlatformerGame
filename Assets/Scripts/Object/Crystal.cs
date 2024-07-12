using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] bool isRandomAum = true;
    [SerializeField, Range(1,5)] int randomAum =2;
    [SerializeField] ParticleSystem electricParticle;
    [SerializeField] SpriteOutline[] outlines;

    [SerializeField] SpriteRenderer crystalSprite;
    [SerializeField] Color afterColor;

    bool isInteractable = false;
    bool isTriggerPlayer =false;
    private void Awake()
    {
        if (isRandomAum)
            randomAum = Random.Range(1, 6);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) &&isTriggerPlayer && !isInteractable)
        {
            GetAum();
        }
    }

    public void GetAum()
    {
        GameManager.Aum_Manager.GetAum(randomAum);
        electricParticle.gameObject.SetActive(false);
        GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, this.transform);
        crystalSprite.color = afterColor;
        for (int i = 0; i < 2; i++)
            outlines[i].enabled = false;
        Destroy(this);
        isInteractable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isInteractable)
        {
            isTriggerPlayer = true;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(true, this.transform);
            for (int i = 0; i < 2; i++)
                outlines[i].enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isInteractable)
        {
            isTriggerPlayer = false;
            if (GameManager.Instance.UI_Controller == null)
                return;
            GameManager.Instance.UI_Controller.Indicator.OnOffUI(false, this.transform);
            for (int i = 0; i < 2; i++)
                outlines[i].enabled = false;
        }
    }
}
