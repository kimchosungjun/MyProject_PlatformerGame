using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MonologueUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI monologueText;
    [SerializeField, TextArea()] string[] firstStageMonologue;
    [SerializeField] float fadeTime = 3f;
    float fadeTimer = 0;

    int curIdx = 0;

    Color textColor;

    public bool IsMonologue { get; set; } = false;

    private void Awake()
    {
        textColor = monologueText.color;   
    }

    public void FirstStateMonologue()
    {
        if (curIdx == 0)
        {
            IsMonologue = true;
            GameManager.Instance.Sound_Manager.StopBGM();
        }
        StartCoroutine(MonologueCor());
    }

    public IEnumerator MonologueCor()
    {
        fadeTimer = 0f;
        monologueText.text = firstStageMonologue[curIdx];
        while (fadeTimer <  fadeTime)
        {
            fadeTimer += Time.deltaTime;
            textColor.a = Mathf.Lerp(0, 1, fadeTimer / fadeTime);
            monologueText.color = textColor;
            yield return null;
        }
        fadeTimer = 0f;
        yield return new WaitForSeconds(1f);
        while (fadeTimer < fadeTime)
        {
            fadeTimer += Time.deltaTime;
            textColor.a = Mathf.Lerp(1, 0, fadeTimer / fadeTime);
            monologueText.color = textColor;
            yield return null;
        }
        curIdx += 1;
        if (curIdx < firstStageMonologue.Length)
            FirstStateMonologue();
        else
            EndMonologue();
    }

    public void EndMonologue()
    {
        IsMonologue = false;
        GameManager.Instance.SaveAllData();
        GameManager.LoadScene_Manager.LoadScene("Lobby");
    }
}
