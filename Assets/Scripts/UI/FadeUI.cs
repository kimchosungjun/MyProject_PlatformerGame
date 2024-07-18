using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField, Range(0,2f)] float fadeTimer;
    Color defaultColor = new Color();

    bool isFadeIn = false;
    bool isFadeOut = false;

    private void Awake()
    {
        if (fadeImage == null)
            fadeImage = GetComponentInChildren<Image>();
        defaultColor = fadeImage.color;
    }

    /// <summary>
    /// Light (alpha = 0)
    /// </summary>
    public void FadeIn() // light : Call when you enter Scene
    {
        if (!isFadeIn)
            StartCoroutine(FadeInCor());
    }

    /// <summary>
    /// Black (alpha = 1)
    /// </summary>
    /// <param name="_sceneName"></param>
    public void FadeOut(string _sceneName) // black : Call when you enter next Scene
    {
        if(!isFadeOut)
            StartCoroutine(FadeOutCor(_sceneName));
    }

    public IEnumerator FadeInCor()
    {
        float timer = 0f;
        isFadeIn = true;
        while (timer < fadeTimer)
        {
            timer += Time.deltaTime;
            defaultColor.a = Mathf.Lerp(1, 0, timer / fadeTimer);
            fadeImage.color = defaultColor;
            yield return null;
        }
        defaultColor.a = 0f;
        fadeImage.color = defaultColor;
        isFadeIn = false;
    }

    public IEnumerator FadeOutCor(string _sceneName)
    {
        float timer = 0f;
        isFadeOut = true;
        while (timer < fadeTimer)
        {
            timer += Time.deltaTime;
            defaultColor.a = Mathf.Lerp(0, 1, timer / fadeTimer); 
            fadeImage.color = defaultColor;
            yield return null;
        }
        defaultColor.a = 1f;
        fadeImage.color = defaultColor;
        GameManager.LoadScene_Manager.LoadScene(_sceneName);
        isFadeOut = false;
    }

    public IEnumerator GameOverFadeOutCor()
    {
        float timer = 0f;
        isFadeOut = true;
        while (timer < fadeTimer)
        {
            timer += Time.deltaTime;
            defaultColor.a = Mathf.Lerp(0, 1, timer / fadeTimer);
            fadeImage.color = defaultColor;
            yield return null;
        }
        defaultColor.a = 1f;
        fadeImage.color = defaultColor;
        isFadeOut = false;
        GameManager.Instance.UI_Controller.Gameover.TurnOnGameOver();
        Time.timeScale = 0f;
    }
}
