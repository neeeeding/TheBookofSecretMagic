using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIdleState : PState
{
    public PIdleState(string animation, PStateMachin machin, Player player) : base(animation, machin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerMobileInput.mousePos += Move;
    }

    private void Move(Vector2 mousePos)
    {
        if (mousePos != Vector2.zero)
        {
            _stateMachin.ChangeState(PlayerState.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
        PlayerMobileInput.mousePos += Move;
    }
}
