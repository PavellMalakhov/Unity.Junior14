using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private PatrolWayPoint _patrolWayPoint;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _enemy.SetCollider2DPlayerHitBox(collision);
            _patrolWayPoint.SetCollider2DPlayerHitBox(collision);
        }
    }
}
