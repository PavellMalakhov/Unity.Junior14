using UnityEngine;

public class PatrolWayPoint : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private Transform _containerPoints;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private int _indexWayPoint;
    [SerializeField] private float _liftForce = 5;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private RaycastHit2D _hitinfo;
    [SerializeField] private Transform _player;

    private Vector3 _scaleLeft = new Vector3(-0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(0.5f, 0.5f, 1f);
    private bool _isPatroling = true;
    private Vector2 targetPoint = Vector2.zero;

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

        Pursuit();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground _))
        {
            _rigidbody2D.velocity = new Vector2(0, _liftForce);
        }
    }

    private void Pursuit()
    {
        _hitinfo = Physics2D.Raycast(transform.position, _player.position - transform.position, (_player.position - transform.position).magnitude, 1);
        Debug.DrawRay(transform.position, _player.position - transform.position, Color.red);

        if (_hitinfo.collider == null && _isPatroling == true)
        {
            Debug.Log(gameObject.name + ": Увидел тебя!");
            targetPoint = _player.position;
            _isPatroling = false;
            _liftForce = 2f;
            _rigidbody2D.gravityScale = 0.05f;
            SetLookDirection(_player.position);
        }

        if (_hitinfo.collider == null && _isPatroling == false)
        {
            Debug.Log(gameObject.name + ": Вижу тебя!");
            targetPoint = _player.position;
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime * 2);
            SetLookDirection(_player.position);
        }

        if (_hitinfo.collider != null && _isPatroling == false)
        {
            Debug.Log(gameObject.name + ": Видел тебя тут: " + targetPoint);
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, _moveSpeed * Time.deltaTime * 2);
            SetLookDirection(targetPoint);
        }
    }

    private void SetLookDirection(Vector2 player)
    {
        if (transform.position.x < player.x)
        {
            transform.localScale = _scaleRight;
        }

        if (transform.position.x > player.x)
        {
            transform.localScale = _scaleLeft;
        }
    }
}
