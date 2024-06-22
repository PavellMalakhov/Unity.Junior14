using UnityEngine;

public class PatrolWayPoint : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private Transform _containerPoints;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private int _indexWayPoint;
    [SerializeField] private float _liftForce = 5;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector3 _scaleLeft = new Vector3(-0.5f, 0.5f, 1f);
    private Vector3 _scaleRight = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private RaycastHit2D _hitinfo;
    [SerializeField] private Transform _player;
    private bool _isPatrol = true;
    [SerializeField] private float _checkDistace = 100f;

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

        if (_isPatrol)
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
        _hitinfo = Physics2D.Raycast(transform.position, _player.position - transform.position, _checkDistace, 1);
        Debug.DrawRay(transform.position, _player.position - transform.position, Color.red);

        //Debug.Log(_hitinfo.collider?.name);

        if (_hitinfo.collider == null)
        {
            Debug.Log(gameObject.name + ": Вижу тебя!");

            //_wayPoints = new Transform[_player.childCount];

            //for (int i = 0; i < _wayPoints.Length; i++)
            //{
            //    _wayPoints[i] = _player.GetChild(i);
            //}

            _isPatrol = false;
            //_liftForce = 0;
            //_rigidbody2D.gravityScale = 0.01f;
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime*10);
        }
    }
}
