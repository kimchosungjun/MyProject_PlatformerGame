using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField, Tooltip("0:노말, 1: 바람, 2: 물")] Player[] players;
    PlayerType currentType = PlayerType.Normal;

    //private string ground;
    //private string jumpPlatform;

    Dictionary<string, JumpPlatform> jumpPlatformDic = new Dictionary<string, JumpPlatform>();

    public void Init(PlayerType _playerType)
    {
        //ground = ConvertEnum.ConvertEnumToString<PaltformType>(PaltformType.Ground);
        //jumpPlatform = ConvertEnum.ConvertEnumToString<PaltformType>(PaltformType.JumpPlatform);
        ChangeType(_playerType);
    }

    public void ChangeType(PlayerType _playerType)
    {
        currentType = _playerType;
    }

    #region Trigger Check
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ground
        if(collision.CompareTag("Ground"))
        {
            players[(int)currentType].IsGround = true;
        }

        if (collision.CompareTag("MovePlatform"))
        {
            players[(int)currentType].IsGround = true;
            players[(int)currentType].gameObject.transform.SetParent(collision.transform);
        }

        if (collision.CompareTag("JumpPlatform"))
        {
            if (jumpPlatformDic.ContainsKey(collision.name))
            { 
                JumpPlatform jumpPlatform = jumpPlatformDic[collision.name];
                players[(int)currentType].Rigid.velocity = new Vector2(players[(int)currentType].Rigid.velocity.x, jumpPlatform.JumpForce);
                jumpPlatform.PlayParticle();
            }
            else
            {
                JumpPlatform jumpPlatform = collision.GetComponent<JumpPlatform>();
                jumpPlatformDic.Add(collision.name, jumpPlatform);
                players[(int)currentType].Rigid.velocity = new Vector2(players[(int)currentType].Rigid.velocity.x, jumpPlatform.JumpForce);
                jumpPlatform.PlayParticle();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Ground
        if (collision.CompareTag("Ground"))
        {
            players[(int)currentType].IsGround = false;
        }

        if (collision.CompareTag("MovePlatform"))
        {
            players[(int)currentType].IsGround = false;
            players[(int)currentType].gameObject.transform.SetParent(null);
        }
    }
    #endregion
}
