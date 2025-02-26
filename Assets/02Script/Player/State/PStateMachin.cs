using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStateMachin
{
    public Dictionary<PlayerState, PState> PStateD = new Dictionary<PlayerState, PState>();

    public PState currentState;

    public void ChangeState(PlayerState state, PlayerRotate rotate)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = PStateD[state];
        currentState.Enter(rotate);
    }

    public void AddState(PlayerState state, PState sc)
    {
        PStateD.Add(state, sc);
    }
}
