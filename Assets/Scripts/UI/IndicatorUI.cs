using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorUI : MonoBehaviour
{
    [SerializeField] GameObject indicator;

    public void OnOffUI(bool _isActve, Transform _tf =null)
    {
        if (_tf == null)
            return;
        if (_isActve)
            transform.position = _tf.position + Vector3.down*2;
        indicator.SetActive(_isActve);
    }
}

