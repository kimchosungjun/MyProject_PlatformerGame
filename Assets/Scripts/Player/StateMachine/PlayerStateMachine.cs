using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState = null;

    public void ChangeState(PlayerState _changeState)
    {
        if(currentState==null)
        {
            currentState = _changeState;
            currentState.Enter();
            return;
        }    
        currentState.Exit();
        currentState = _changeState;
        currentState.Enter();
    }
    
    public void Execute()
    {
        if(currentState!=null)
            currentState.Execute();
    }
}
