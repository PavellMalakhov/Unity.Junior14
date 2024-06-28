using UnityEngine;

public class PatrolWayPoint : MonoBehaviour
{
    [SerializeField] private Pursuit _pursuit;
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private Transform _containerPoints;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private int _indexWayPoint;
    [SerializeField] private float _liftForce = 5;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Collider2D _player;
    private Vector3 _scaleLeft = new Vector3(-0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(0.5f, 0.5f, 1f);
    private bool _isPatroling = true;

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
            _indexWayPoint = ++_indexWayPoint % _wayPoints.Length;
        }

        if (_isPatroling)
        {
            transform.position = Vector2.MoveTowards(transform.position, _wayPoints[_indexWayPoint].position, _moveSpeed * Time.deltaTime);
        }

        if (transform.position.x < _wayPoints[_indexWayPoint].position.x)
        {
            transform.localScale = _scaleRight;
        }
        else
        {
            transform.localScale = _scaleLeft;
        }

        _pursuit.PursuitPlayer(_moveSpeed, _liftForce, _rigidbody2D, _scaleLeft, _scaleRight, ref _isPatroling, _player);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground _))
        {
            _rigidbody2D.velocity = new Vector2(0, _liftForce);
        }
    }

    public void SetCollider2DPlayerHitBox(Collider2D collision)
    {
        _player = collision;
    }
}
