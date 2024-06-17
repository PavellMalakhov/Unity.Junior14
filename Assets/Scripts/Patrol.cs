using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private bool _directionRight = true;

    private Vector2 _directionMove = Vector2.right;
    private Vector3 _scaleLeft = new Vector3(-0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(0.5f, 0.5f, 1f);

    private void Update()
    {
        transform.Translate(_directionMove * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
        {
            _directionRight = !_directionRight;

            if (_directionRight)
            {
                _directionMove = Vector2.right;
                transform.localScale = _scaleRight;
            }
            else
            {
                _directionMove = Vector2.left;
                transform.localScale = _scaleLeft;
            }
        }
    }
}
