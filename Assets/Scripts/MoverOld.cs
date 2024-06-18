using UnityEngine;
using System;

public class MoverOld : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    private readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 450;
    [SerializeField] private bool _isGround = false;
    [SerializeField] private bool _isJump = false;

    private Vector2 _directionMove;
    private Vector3 _scaleLeft = new Vector3(0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(-0.5f, 0.5f, 1f);

    private void Update()
    {
        Move();

        if (Convert.ToBoolean(Input.GetAxis(Jump)) && _isGround)
        {
            _isJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            _isGround = false;
            _isJump = false;

            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Ground ground))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGround = false;
    }

    private void Move()
    {
        _directionMove = new Vector2(Input.GetAxis(Horizontal), 0);

        if (_directionMove.magnitude == 0)
        {
            _animator.SetBool(IsWalk, false);

            return;
        }
        else
        {
            transform.Translate(_directionMove * _moveSpeed * Time.deltaTime);

            _animator.SetBool(IsWalk, true);

            if (_directionMove.x < 0)
            {
                transform.localScale = _scaleLeft;
            }

            if (_directionMove.x > 0)
            {
                transform.localScale = _scaleRight;
            }
        }
    }
}