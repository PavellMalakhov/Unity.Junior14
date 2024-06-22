using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private readonly int HeroAttack = Animator.StringToHash(nameof(HeroAttack));
    private readonly int Damage = Animator.StringToHash(nameof(Damage));

    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _player;

    private float _health = 100;
    private float _forceAttack = 10f;
    private Vector2 _directionAttack = new Vector2(10, 10);

    public void TakeDamage(float damage)
    {
        _health -= damage;

        _animator.Play(Damage);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetInstanceID() == _player.GetInstanceID() && collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(_forceAttack);

            collision.attachedRigidbody.velocity = _directionAttack;

            _animator.Play(HeroAttack);
        }
    }
}
