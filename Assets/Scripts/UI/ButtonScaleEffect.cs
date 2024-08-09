using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private float scaleIncreasment = 1.2f;
    [SerializeField] Transform textTransform;
    [SerializeField] Button btn;
    private void Awake()
    {
        originalScale = textTransform.localScale;
        if (btn == null)
            btn = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!btn.interactable)
            return;
        textTransform.localScale = originalScale * scaleIncreasment;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!btn.interactable)
            return;
        textTransform.localScale = originalScale;
    }
}
