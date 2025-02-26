using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : PState
{
    private Vector2 targetPos; //���콺 ��ġ
    private float speed = 1; //�ӵ�
    public PMoveState(string animation, PStateMachin machin, Player player) : base(animation, machin, player)
    {
    }

    public override void Enter(PlayerRotate rotate)
    {
        base.Enter(rotate);
        PlayerMobileInput.mousePos += Move;
    }

    public override void StateFixedUpdate() //������
    {
        base.StateFixedUpdate();
        _player.transform.position = Vector2.Lerp(_player.transform.position, targetPos, speed * Time.deltaTime);

        if(Vector2.Distance(_player.transform.position, targetPos) < 1f)
        {
            _stateMachin.ChangeState(PlayerState.Idle,_rotate);
        }
    }

    private void Move(Vector2 mousePos) //�������� ���� ��ġ ����(�ִϸ��̼�)
    {
        targetPos = mousePos;

        ChangeAnimation(mousePos);

        if (_player.transform.position.x == targetPos.x && _player.transform.position.y == targetPos.y)
        {
            _stateMachin.ChangeState(PlayerState.Idle, _rotate);
        }
    }

    private void ChangeAnimation(Vector2 mousePos) //�ִ� �ٲٱ�
    {
        _player.Animator.SetBool(Animator.StringToHash(_rotate.ToString()), false); //�ϴ� ����

        _rotate = Mathf.Abs(mousePos.x - _player.transform.position.x) <= Mathf.Abs(mousePos.y - _player.transform.position.y) ? //�ѱ�
               mousePos.y - _player.transform.position.y <= 0 ? PlayerRotate.Back : PlayerRotate.Front :
               mousePos.x - _player.transform.position.x <= 0 ? PlayerRotate.Left : PlayerRotate.Right;

        _player.Animator.SetBool(Animator.StringToHash(_rotate.ToString()), true); //�ѱ�
    }

    public override void Exit()
    {
        base.Exit();
        PlayerMobileInput.mousePos += Move;
    }
}
