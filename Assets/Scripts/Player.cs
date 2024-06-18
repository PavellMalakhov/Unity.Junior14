using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));

    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Animator _animator;

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
    }
}
