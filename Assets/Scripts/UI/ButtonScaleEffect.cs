using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private float scaleIncreasment = 1.2f;
    [SerializeField] Transform textTransform;

    private void Awake()
    {
        originalScale = textTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textTransform.localScale = originalScale * scaleIncreasment;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textTransform.localScale = originalScale;
    }
}
