using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 9;

    private Vector2 _directionMove;
    private Vector3 _scaleLeft = new Vector3(0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(-0.5f, 0.5f, 1f);

    public void Jump()
    {
        _rigidbody2D.velocity = new Vector2(0, _jumpForce);
    }

    public void Move(float direction)
    {
        _directionMove = new Vector2(direction, 0);

        transform.Translate(_directionMove * _moveSpeed * Time.deltaTime);

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
