using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField, Tooltip("0 : idle, 1 : press")] List<Texture2D> mouseCursor;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.SetCursor(mouseCursor[1], new Vector2(mouseCursor[1].width * 0.5f, mouseCursor[1].height * 0.5f), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(mouseCursor[0], new Vector2(mouseCursor[1].width * 0.5f, mouseCursor[1].height * 0.5f), CursorMode.Auto);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameManager.Instance.Sound_Manager.PlayUISFX(UISoundType.Click);
        }
    }
}
