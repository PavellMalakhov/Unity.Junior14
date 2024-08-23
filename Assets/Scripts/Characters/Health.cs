using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _current = 100f;
    [SerializeField] private float _max = 100f;
    [SerializeField] private float _min = 0f;

    public event Action<float, float> Changed;

    private void Start()
    {
        Changed?.Invoke(_current, _max);
    }

    public void AddHealth(float amount)
    {
        if (amount > 0)
        {
            _current = Mathf.Clamp(_current + amount, _min, _max);

            Changed?.Invoke(_current, _max);
        }
    }

    public float TakeDamage(float damage)
    {
        if (damage < 0)
            return 0;

        float delta = _current;

        _current = Mathf.Clamp(_current - damage, _min, _max);

        delta -= _current;

        Changed?.Invoke(_current, _max);

        if (_current <= 0)
        {
            Destroy(gameObject);
        }

        return delta;
    }
}