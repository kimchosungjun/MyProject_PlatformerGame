using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoramlState { }

namespace PlayerNormalStateSpace
{
    #region Idle
    public class NormalIdle : PlayerState
    {
        NormalPlayer player;
        PlayerData data;

        string currentAnimName = "";
        float horizontal;
        public NormalIdle(NormalPlayer _player) :base(_player)
        {
            player = _player;
            currentAnimName = ConvertEnum.ConvertEnumToString(PlayerActionType.Idle);
            data = player.PData;
        }

        public override void Enter()
        {
            player.Anim.SetBool(currentAnimName, true);
        }

        public override void Execute()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if (!player.IsGround)
            {
                if (player.Rigid.velocity.y > 0)
                {
                    player.ChangeActionState(PlayerActionType.Jump);
                    return;
                }
                else if (player.Rigid.velocity.y < 0)
                {
                    player.ChangeActionState(PlayerActionType.Fall);
                    return;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, data.ySpeed);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift) && player.CanRoll())
                {
                    player.ChangeActionState(PlayerActionType.Roll);
                    return;
                }
                if (horizontal != 0)
                {
                    player.ChangeActionState(PlayerActionType.Move);
                    return;
                }
            }
        }

        public override void Exit()
        {
            player.Anim.SetBool(currentAnimName, false);
        }
    }
    #endregion

    #region Move
    public class NormalMove : PlayerState
    {
        NormalPlayer player;
        PlayerData data;

        string currentAnimName = "";
        float horizontal;
        public NormalMove(NormalPlayer _player) : base(_player)
        {
            player = _player;
            currentAnimName = ConvertEnum.ConvertEnumToString(PlayerActionType.Move);
            data = player.PData;
        }

        public override void Enter()
        {
            player.Anim.SetBool(currentAnimName, true);
        }

        public override void Execute()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            player.Rigid.velocity = new Vector2(horizontal*data.xSpeed, player.Rigid.velocity.y);
            if (!player.IsGround)
            {
                if (player.Rigid.velocity.y > 0)
                {
                    player.ChangeActionState(PlayerActionType.Jump);
                    return;
                }
                else if (player.Rigid.velocity.y < 0)
                {
                    player.ChangeActionState(PlayerActionType.Fall);
                    return;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, data.ySpeed);
                    return;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift) && player.CanRoll())
                {
                    player.ChangeActionState(PlayerActionType.Roll);
                    return;
                }
                if (horizontal == 0)
                {
                    player.ChangeActionState(PlayerActionType.Idle);
                    return;
                }
            }
        }

        public override void Exit()
        {
            player.Anim.SetBool(currentAnimName, false);
        }
    }
    #endregion

    #region Jump
    public class NormalJump : PlayerState
    {
        NormalPlayer player;
        PlayerData data;

        string currentAnimName = "";
        float horizontal;

        public NormalJump(NormalPlayer _player) : base(_player)
        {
            player = _player;
            currentAnimName = ConvertEnum.ConvertEnumToString(PlayerActionType.Jump);
            data = player.PData;
        }

        public override void Enter()
        {
            player.Anim.SetBool(currentAnimName, true);
        }

        public override void Execute()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            player.Rigid.velocity = new Vector2(horizontal * data.xSpeed, player.Rigid.velocity.y);
            if (!player.IsGround)
            {
                 if (player.Rigid.velocity.y < 0)
                {
                    player.ChangeActionState(PlayerActionType.Fall);
                    return;
                }
            }
            else
            {
                if (Mathf.Abs(player.Rigid.velocity.x) > 0)
                {
                    player.ChangeActionState(PlayerActionType.Move);
                    return;
                }
                else
                {
                    player.ChangeActionState(PlayerActionType.Idle);
                    return;
                }
            }
        }

        public override void Exit()
        {
            player.Anim.SetBool(currentAnimName, false);
        }
    }
    #endregion

    #region Fall
    public class NormalFall: PlayerState
    {
        NormalPlayer player;
        PlayerData data;

        string currentAnimName = "";
        float horizontal;
        
        public NormalFall(NormalPlayer _player) : base(_player)
        {
            player = _player;
            currentAnimName = ConvertEnum.ConvertEnumToString(PlayerActionType.Fall);
            data = player.PData;
        }

        public override void Enter()
        {
            player.Anim.SetBool(currentAnimName, true);
        }

        public override void Execute()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            player.Rigid.velocity = new Vector2(data.xSpeed*horizontal,player.Rigid.velocity.y);
            if (!player.IsGround)
            {
                if (player.Rigid.velocity.y > 0)
                {
                    player.ChangeActionState(PlayerActionType.Jump);
                    return;
                }
            }
            else
            {
                if (Mathf.Abs(player.Rigid.velocity.x) > 0)
                {
                    player.ChangeActionState(PlayerActionType.Move);
                    return;
                }
                else
                {
                    player.ChangeActionState(PlayerActionType.Idle);
                    return;
                }
            }
        }
        public override void Exit()
        {
            player.Anim.SetBool(currentAnimName, false);
        }
    }
    #endregion

    #region Roll
    public class NormalRoll : PlayerState
    {
        NormalPlayer player;
        PlayerData playerData;

        string currentAnimName = "";
        float timer = 0f;

        public NormalRoll(NormalPlayer _player) : base(_player)
        {
            player = _player;
            currentAnimName = ConvertEnum.ConvertEnumToString(PlayerActionType.Roll);
            playerData = _player.PData;
        }

        public override void Enter()
        {
            player.Anim.SetBool(currentAnimName, true);
            player.Invinsibility(playerData.rollTime);
            player.IsRoll = true;
            timer = 0f;
        }

        public override void Execute()
        {
            timer += Time.deltaTime;
            if (timer < playerData.rollTime)
            {
                player.Rigid.velocity = new Vector2(playerData.rollSpeed * player.transform.localScale.x, player.Rigid.velocity.y);
            }
            else
            {
                if (!player.IsGround)
                {
                    if (player.Rigid.velocity.y > 0)
                    {
                        player.ChangeActionState(PlayerActionType.Jump);
                        return;
                    }
                    else if (player.Rigid.velocity.y < 0)
                    {
                        player.ChangeActionState(PlayerActionType.Fall);
                        return;
                    }
                }
                else
                {
                    if (Mathf.Abs(player.Rigid.velocity.x) > 0)
                    {
                        player.ChangeActionState(PlayerActionType.Move);
                        return;
                    }
                    else
                    {
                        player.ChangeActionState(PlayerActionType.Idle);
                        return;
                    }
                }
            }
        }
        public override void Exit()
        {
            player.Anim.SetBool(currentAnimName, false);
            player.IsRoll = false;
        }
    }
    #endregion
}


