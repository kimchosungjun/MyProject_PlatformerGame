using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayer : Player
{
    PlayerController playerController;

    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    
    [SerializeField] float horizontal;
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime = 3f;
    [SerializeField] float dashCoolTime = 10f;

    int jumpCnt = 0; // 이단점프
    bool isJump = false; // 점프
    bool canDash = true;
    bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    public void Init(PlayerController _playerController) // = Awake
    {
        playerController = _playerController;
    }

    public void Execute() // = Update
    {
        if (canMove)
        {
            XMove();
            Flip();
            YMove();
            Dash();
        }
    }

    public void XMove()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * xSpeed, rb.velocity.y);
    }

    public void Flip()
    {
        if (horizontal == 1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal == -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void YMove()
    {
        if (IsGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, ySpeed);
        }
    }
    
    public void Dash()
    {
        if(canDash && IsGround && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            CanNotMove(false);
            StartCoroutine(DashCor());
        }
    }

    public IEnumerator DashCor()
    {
        canDash = false;
        canMove = false;
        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);
        // 대쉬 지속시간
        yield return new WaitForSeconds(dashTime);
        canMove = true;
        // 대쉬 쿨타임
        yield return new WaitForSeconds(dashCoolTime);
        canDash = true;
    }

    public void CanNotMove(bool _isVectorZero)
    {
        horizontal = 0;
        if(_isVectorZero)
            rb.velocity = new Vector2(0,rb.velocity.y);
        else
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }
}
