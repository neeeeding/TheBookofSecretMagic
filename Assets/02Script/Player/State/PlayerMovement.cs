using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;

public class PlayerMovement: Singleton<PlayerMovement>
{
    [SerializeField] private float speed; //속도
    [HideInInspector] public Vector2 TargetPos; //마우스 위치
    private Player _player;
    private Rigidbody2D _rigidbody;
    private bool _isMoving;

    #region endiaw

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _isMoving = false;
        PlayerMobileInput.mousePos += Move;
    }

    private void OnDisable()
    {
        PlayerMobileInput.mousePos -= Move;
    }
    #endregion

    private void FixedUpdate()
    {
        Vector2 direction = (TargetPos - (Vector2)_player.transform.position);

        if (direction.magnitude < 0.1f || !_isMoving) // 너무 가까우면 멈추기
        {
            _rigidbody.velocity = Vector2.zero;
            _isMoving = false;
        }
        else
        {
            _rigidbody.velocity = direction.normalized * speed;
        }
    }

    private void Move(Vector2 mousePos)
    {
        _isMoving = true;
        TargetPos = mousePos;
    }
}
