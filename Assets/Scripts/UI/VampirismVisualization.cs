using System.Collections;
using UnityEngine;

public class VampirismVisualization : MonoBehaviour
{
    private readonly int _vampirismAnimation = Animator.StringToHash(nameof(_vampirismAnimation));

    [SerializeField] private Animator _animator;
    [SerializeField] private float _delay = 1;
    [SerializeField] private Coroutine _animation;
    [SerializeField] private Vampirism _vampirism;

    private float _oldValueMana = float.MaxValue;

    protected void OnEnable()
    {
        _vampirism.ManaChanged += StartAnimation;
    }

    protected void OnDisable()
    {
        _vampirism.ManaChanged -= StartAnimation;
    }

    public void StartAnimation(float absorbMana, float absorbManaMax)
    {
        if (_animation == null && absorbMana < _oldValueMana)
        {
            _animation = StartCoroutine(Animation());
        }

        _oldValueMana = absorbMana;
    }

    private IEnumerator Animation()
    {
        var wait = new WaitForSeconds(_delay);

        _animator.Play(_vampirismAnimation);

        yield return wait;

        _animation = null;
    }
}
