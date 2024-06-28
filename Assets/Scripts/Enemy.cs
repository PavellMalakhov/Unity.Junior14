using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int HeroAttack = Animator.StringToHash(nameof(HeroAttack));
    private readonly int Damage = Animator.StringToHash(nameof(Damage));

    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private Collider2D _enemy;

    private Collider2D _player;
    private float _forceAttack = 1f;
    private Vector2 _directionAttack = new Vector2(10, 10);

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && collision.IsTouching(_enemy))
        {
            _animator.Play(HeroAttack);

            player.TakeDamage(_forceAttack);

            collision.attachedRigidbody.velocity = _directionAttack;
        }
    }

    public void TakeDamage(float damage)
    {
        _animator.Play(Damage);

        if (_health.TakeDamage(damage))
        {
            Destroy(gameObject);
        }
    }

    public void SetCollider2DPlayerHitBox(Collider2D collision)
    {
        _player = collision;
    }
}
