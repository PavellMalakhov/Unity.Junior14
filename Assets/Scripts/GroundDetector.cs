using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private readonly int Jump = Animator.StringToHash(nameof(Jump));
    private readonly int Idle = Animator.StringToHash(nameof(Idle));

    [SerializeField] private Animator _animator;

    public bool IsGround { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _animator.Play(Idle);

            IsGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _animator.Play(Jump);

            IsGround = false;
        }
    }
}
