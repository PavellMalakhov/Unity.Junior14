using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarSmooth : EventHandler
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _lerpTime = 5f;

    private float _targetHealth;

    protected override void RenderHealh(float health, float healthMax)
    {
        StopAllCoroutines();
   
        _targetHealth = health / healthMax;

        StartCoroutine(SmoothRenderHealh(_targetHealth));
    }

    private IEnumerator SmoothRenderHealh(float targetHealth)
    {
        var wait = new WaitForEndOfFrame();

        while (_slider.value != targetHealth)
        {
            _slider.value = Mathf.Lerp(_slider.value, targetHealth, _lerpTime * Time.deltaTime);

            yield return wait;
        }
    }
}