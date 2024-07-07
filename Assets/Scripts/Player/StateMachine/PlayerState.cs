using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    public PlayerState(Player _player)
    {
        player = _player;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
