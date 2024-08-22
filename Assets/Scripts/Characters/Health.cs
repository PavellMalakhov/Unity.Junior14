using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthCurrent = 100f;
    [SerializeField] private float _healthMax = 100f;
    [SerializeField] private float _healthMin = 0f;

    public event Action<float, float> HealthChanged;

    private void Start()
    {
        HealthChanged?.Invoke(_healthCurrent, _healthMax);
    }

    public void AddHealth(float amountHealth)
    {
        if (amountHealth > 0)
        {
            _healthCurrent = Mathf.Clamp(_healthCurrent + amountHealth, _healthMin, _healthMax);

            HealthChanged?.Invoke(_healthCurrent, _healthMax);
        }
    }

    public float TakeDamage(float damage)
    {
        if (damage < 0)
            return 0;

        float healthChange = _healthCurrent;

        _healthCurrent = Mathf.Clamp(_healthCurrent - damage, _healthMin, _healthMax);

        healthChange -= _healthCurrent;

        HealthChanged?.Invoke(_healthCurrent, _healthMax);

        if (_healthCurrent <= 0)
        {
            Destroy(gameObject);
        }

        return healthChange;
    }
}