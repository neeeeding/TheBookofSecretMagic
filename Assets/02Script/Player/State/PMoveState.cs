using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : PState
{
    private Vector2 targetPos; //마우스 위치
    private float speed = 1; //속도
    public PMoveState(string animation, PStateMachin machin, Player player) : base(animation, machin, player)
    {
    }

    public override void Enter(PlayerRotate rotate)
    {
        base.Enter(rotate);
        PlayerMobileInput.mousePos += Move;
    }

    public override void StateFixedUpdate() //움직임
    {
        base.StateFixedUpdate();
        _player.transform.position = Vector2.Lerp(_player.transform.position, targetPos, speed * Time.deltaTime);

        if(Vector2.Distance(_player.transform.position, targetPos) < 0.5f)
        {
            _stateMachin.ChangeState(PlayerState.Idle,_rotate);
        }
    }

    private void Move(Vector2 mousePos) //움직임을 위한 위치 지정(애니메이션)
    {
        targetPos = mousePos;

        ChangeAnimation(mousePos);

        if (_player.transform.position.x == targetPos.x && _player.transform.position.y == targetPos.y)
        {
            _stateMachin.ChangeState(PlayerState.Idle, _rotate);
        }
    }

    private void ChangeAnimation(Vector2 mousePos) //애니 바꾸기
    {
        _player.Animator.SetBool(Animator.StringToHash(_rotate.ToString()), false); //일단 끄고

        Vector2 minusPos = new Vector2(mousePos.x - _player.transform.position.x , mousePos.y - _player.transform.position.y);


        _rotate = Mathf.Abs(minusPos.x) > Mathf.Abs(minusPos.y) ? //켜기
               minusPos.x <= 0 ? PlayerRotate.Left : PlayerRotate.Right :
               minusPos.y <= 0 ? PlayerRotate.Back : PlayerRotate.Front;

        _player.Animator.SetBool(Animator.StringToHash(_rotate.ToString()), true); //켜기
    }

    public override void Exit()
    {
        base.Exit();
        PlayerMobileInput.mousePos += Move;
    }
}
