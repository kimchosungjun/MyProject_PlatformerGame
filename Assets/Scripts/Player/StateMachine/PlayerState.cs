using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public PlayerState(Player _player) { }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
