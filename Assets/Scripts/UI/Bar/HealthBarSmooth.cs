using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmooth : EventHandler
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _lerpSpeed = 1f;

    private Coroutine _renderHealth;

    protected override void RenderHealh(float health, float healthMax)
    {
        RenderBar(health, healthMax, _renderHealth, _lerpSpeed, _slider);    
    }
}