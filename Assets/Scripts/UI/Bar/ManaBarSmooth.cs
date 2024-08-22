using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaBarSmooth : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _lerpSpeed = 1f;
    [SerializeField] private Vampirism _vampirism;

    private float _targetMana;
    private Coroutine _renderMana;

    protected void OnEnable()
    {
        _vampirism.ManaChanged += RenderMana;
    }

    protected void OnDisable()
    {
        _vampirism.ManaChanged -= RenderMana;
    }

    private void RenderMana(float absorbMana, float absorbManaMax)
    {
        if (_renderMana != null)
        {
            StopCoroutine(_renderMana);
        }

        _targetMana = absorbMana / absorbManaMax;

        _renderMana = StartCoroutine(SmoothRenderMana(_targetMana, _slider.value));
    }

    private IEnumerator SmoothRenderMana(float targetMana, float startMana)
    {
        var wait = new WaitForEndOfFrame();

        float lerpTime = 0f;

        while (_slider.value != targetMana)
        {
            _slider.value = Mathf.Lerp(startMana, targetMana, lerpTime += _lerpSpeed * Time.deltaTime);

            yield return wait;
        }
    }
}
