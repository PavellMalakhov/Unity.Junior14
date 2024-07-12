using UnityEngine;
using UnityEngine.UI;

public class HealthBar : EventHandler
{
    [SerializeField] private Slider _slider;

    protected override void RenderHealh(float health, float healthMax)
    {
        _slider.value = health / healthMax;
    }
}