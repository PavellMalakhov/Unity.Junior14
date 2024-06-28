using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
    private readonly int Attack = Animator.StringToHash(nameof(Attack));
    private readonly int Damage = Animator.StringToHash(nameof(Damage));

    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Animator _animator;
    [SerializeField] private Combat _combat;
    [SerializeField] private Health _health;
    
    private float _forceAttack = 10f;

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _animator.SetBool(IsWalk, true);

            _mover.Move(_inputReader.Direction);
        }
        else
        {
            _animator.SetBool(IsWalk, false);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
        }

        if (_inputReader.GetAttack())
        {
            _animator.Play(Attack);

            _combat.Attack(_forceAttack);
        }
    }

    public void TakeDamage(float damage)
    {
        _animator.Play(Damage);

        _health.TakeDamage(damage);
    }
}
