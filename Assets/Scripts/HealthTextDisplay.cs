using UnityEngine;
using TMPro;

public class HealthTextDisplay : EventHandler
{
    [SerializeField] private TextMeshProUGUI _currentMaxHealth;

    protected override void RenderHealh(float health, float healthMax)
    {
        _currentMaxHealth.text = health + " / " + healthMax;
    }
}