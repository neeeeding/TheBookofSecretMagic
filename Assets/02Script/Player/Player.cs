using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;

    private PStateMachin stateMachin;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        stateMachin = new PStateMachin();
        stateMachin.AddState(PlayerState.Move, new PMoveState("Move", stateMachin, this));
        stateMachin.AddState(PlayerState.Idle, new PIdleState("Idle", stateMachin, this));
        stateMachin.AddState(PlayerState.hold, new PHoldState("Hold", stateMachin, this));

        stateMachin.ChangeState(PlayerState.Idle, PlayerRotate.Front);
    }

    private void Update()
    {
        stateMachin.currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        stateMachin.currentState.StateFixedUpdate();
    }
}
