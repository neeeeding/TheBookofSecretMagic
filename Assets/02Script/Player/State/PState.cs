using UnityEngine;

public class PState
{
    protected Player _player; //ÇÃ·¹ÀÌ¾î
    protected PStateMachin _stateMachin; //¸Ó½Å
    protected int _animationHash; //ÇØ½Ã

    protected PlayerRotate _rotate; //¾îµô ºÁ¾ß´ÂÁö

    public string _animationName; //°Á Áö±Ý ¹ºÁö ¸ð¸£ÀÝ¾Æ.

    public PState(string animation,PStateMachin machin,Player player)
    {
        _animationName = animation;
        _stateMachin = machin;
        _player = player;
        _animationHash = Animator.StringToHash(animation);
    }

    public virtual void Enter(PlayerRotate rotate)
    {
        _rotate = rotate;
        int rotateHash = Animator.StringToHash(rotate.ToString());
        _player.Animator.SetBool(_animationHash, true);
        _player.Animator.SetBool(rotateHash, true);
    }

    public virtual void Exit()
    {
        int rotateHash = Animator.StringToHash(_rotate.ToString());
        _player.Animator.SetBool(_animationHash, false);
        _player.Animator.SetBool(rotateHash, false);
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

public enum PlayerRotate
{
    Front,Back,Left,Right
}
