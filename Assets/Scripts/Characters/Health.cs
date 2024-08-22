using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _current = 100f;
    [SerializeField] private float _max = 100f;
    [SerializeField] private float _min = 0f;

    public event Action<float, float> HealthChanged;

    private void Start()
    {
        HealthChanged?.Invoke(_current, _max);
    }

    public void AddHealth(float amount)
    {
        if (amount > 0)
        {
            _current = Mathf.Clamp(_current + amount, _min, _max);

            HealthChanged?.Invoke(_current, _max);
        }
    }

    public float TakeDamage(float damage)
    {
        if (damage < 0)
            return 0;

        float delta = _current;

        _current = Mathf.Clamp(_current - damage, _min, _max);

        delta -= _current;

        HealthChanged?.Invoke(_current, _max);

        if (_current <= 0)
        {
            Destroy(gameObject);
        }

        return delta;
    }
}