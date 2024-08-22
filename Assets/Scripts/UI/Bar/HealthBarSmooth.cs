using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarSmooth : EventHandler
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _lerpSpeed = 1f;

    private float _targetHealth;
    private Coroutine _renderHealth;

    protected override void RenderHealh(float health, float healthMax)
    {
        if (_renderHealth != null)
        {
            StopCoroutine(_renderHealth);
        }
   
        _targetHealth = health / healthMax;

        _renderHealth = StartCoroutine(SmoothRenderHealh(_targetHealth, _slider.value));
    }

    private IEnumerator SmoothRenderHealh(float targetHealth, float startHealth)
    {
        var wait = new WaitForEndOfFrame();

        float lerpTime = 0f;

        while (_slider.value != targetHealth)
        {
            _slider.value = Mathf.Lerp(startHealth, targetHealth, lerpTime += _lerpSpeed * Time.deltaTime);

            yield return wait;
        }
    }
}