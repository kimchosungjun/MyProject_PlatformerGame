using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayer : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    PlayerController playerController;

    [SerializeField] float xInput;
    [SerializeField] float yInput;

    public void Init(PlayerController _playerController)
    {
        //anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        playerController = _playerController;
    }

    /// <summary>
    /// Update문에서 계속 호출할 함수 : Update문과 같음
    /// </summary>
    public void Execute()
    {
        XMove();
        YMove();
    }

    public void XMove()
    {
        xInput = Input.GetAxisRaw("Horizontal");
    }

    public void YMove()
    {
        yInput = Input.GetAxisRaw("Vertical");
    }
}
