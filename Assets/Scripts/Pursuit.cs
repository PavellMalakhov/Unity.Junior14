using UnityEngine;

public class Pursuit : MonoBehaviour
{
    [SerializeField] private RaycastHit2D _hitinfo;

    private Vector2 targetPoint = Vector2.zero;
    private Vector3 _transformPivotCorrection = new Vector3(0, 2f, 0);

    public void PursuitPlayer(float moveSpeed, float liftForce, Rigidbody2D rigidbody2D, Vector3 scaleLeft, Vector3 scaleRight, ref bool isPatroling, Collider2D player)
    {
        if (player != null)
        {
            var layerMask = 1 << 8;
            _hitinfo = Physics2D.Raycast(transform.position, player.transform.position + _transformPivotCorrection - transform.position, (player.transform.position - transform.position).magnitude, layerMask);
            Debug.DrawRay(transform.position, player.transform.position + _transformPivotCorrection - transform.position, Color.red);

            if (_hitinfo.collider == null && isPatroling == true)
            {
                Debug.Log(gameObject.name + ": Увидел тебя!");

                targetPoint = player.transform.position;
                isPatroling = false;
                liftForce = 2f;
                rigidbody2D.gravityScale = 0.05f;

                SetLookDirection(player.transform.position, scaleLeft, scaleRight);
            }

            if (_hitinfo.collider == null && isPatroling == false)
            {
                Debug.Log(gameObject.name + ": Вижу тебя!");

                targetPoint = player.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime * 2);
                SetLookDirection(player.transform.position, scaleLeft, scaleRight);
            }

            if (_hitinfo.collider != null && isPatroling == false)
            {
                Debug.Log(gameObject.name + ": Видел тебя тут: " + targetPoint);

                transform.position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime * 2);
                SetLookDirection(targetPoint, scaleLeft, scaleRight);
            }
        }
    }

    private void SetLookDirection(Vector2 target, Vector3 scaleLeft, Vector3 scaleRight)
    {
        if (transform.position.x < target.x)
        {
            transform.localScale = scaleRight;
        }

        if (transform.position.x > target.x)
        {
            transform.localScale = scaleLeft;
        }
    }
}
