using UnityEngine;

public class PatrolWayPoint : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private Transform _containerPoints;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private int _indexWayPoint;
    [SerializeField] private float _liftForce = 5;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    //private Vector2 _directionMove = Vector2.right;
    private Vector3 _scaleLeft = new Vector3(-0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(0.5f, 0.5f, 1f);

    public void Awake()
    {
        _wayPoints = new Transform[_containerPoints.childCount];

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i] = _containerPoints.GetChild(i);
        }
    }

    private void Update()
    {
        if (transform.position == _wayPoints[_indexWayPoint].position)
        {
            _indexWayPoint = (_indexWayPoint + 1) % _wayPoints.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position, _wayPoints[_indexWayPoint].position, _moveSpeed * Time.deltaTime);

        if (transform.position.x < _wayPoints[_indexWayPoint].position.x)
        {
            transform.localScale = _scaleRight;
        }
        else
        {
            transform.localScale = _scaleLeft;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground _) || collision.TryGetComponent(out Player _))
        {
            _rigidbody2D.velocity = new Vector2(0, _liftForce);
        }
    }
}
