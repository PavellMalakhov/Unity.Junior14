using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private bool _directionRight = true;

    private Vector3 _scaleLeft = new Vector3(-0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(0.5f, 0.5f, 1f);

    private void Update()
    {
        if (_directionRight)
        {
            transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * _moveSpeed * Time.deltaTime);
        }

        if (!_directionRight)
        {
            transform.localScale = _scaleLeft;
        }

        if (_directionRight)
        {
            transform.localScale = _scaleRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _directionRight = !_directionRight;
    }
}
