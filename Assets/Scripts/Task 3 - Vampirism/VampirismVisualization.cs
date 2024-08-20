using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismVisualization : MonoBehaviour
{
    private readonly int _vampirismAnimation = Animator.StringToHash(nameof(_vampirismAnimation));

    [SerializeField] private Slider _skillBar;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _delay = 1;
    [SerializeField] private Coroutine _animation;

    public void SetSkillBar(float value)
    {
        _skillBar.value = value;
    }

    public void StartAnimation(float absorbTime)
    {
        _animation = StartCoroutine(Animation(absorbTime));
    }

    public void StopAnimation()
    {
        StopCoroutine(_animation);
    }

    private IEnumerator Animation(float absorbTime)
    {
        var wait = new WaitForSeconds(_delay);

        for (int i = 0; i < absorbTime; i++)
        {
            _animator.Play(_vampirismAnimation);

            yield return wait;
        }
    }
}
