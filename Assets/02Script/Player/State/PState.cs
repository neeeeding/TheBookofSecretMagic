using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PState
{
    protected Player _player;
    protected PStateMachin _stateMachin;
    protected int _animationHash;

    public string _animationName;

    public PState(string animation,PStateMachin machin,Player player)
    {
        _animationName = animation;
        _stateMachin = machin;
        _player = player;
        _animationHash = Animator.StringToHash(animation);
    }

    public virtual void Enter()
    {
        _player.Animator.SetBool(_animationHash, true);
    }

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animationHash, false);
    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateFixedUpdate()
    {

    }
}

public enum PlayerState
{
    Idle, Move, Chat, hold
}
