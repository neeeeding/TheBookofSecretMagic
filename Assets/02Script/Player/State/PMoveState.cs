using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : PState
{
    private Vector2 targetPos;
    private float speed = 1;
    public PMoveState(string animation, PStateMachin machin, Player player) : base(animation, machin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerMobileInput.mousePos += Move;
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        _player.transform.position = Vector2.Lerp(_player.transform.position, targetPos, speed * Time.deltaTime);
    }

    private void Move(Vector2 mousePos)
    {
        targetPos = mousePos;
        if(_player.transform.position.x == targetPos.x && _player.transform.position.y == targetPos.y)
        {
            _stateMachin.ChangeState(PlayerState.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        PlayerMobileInput.mousePos += Move;
    }
}
