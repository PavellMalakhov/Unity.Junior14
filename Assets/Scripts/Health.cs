using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _healthMax = 100f;
    [SerializeField] private float _healthMin = 0f;

    public void AddHealth(float amountHealth)
    {
        if (amountHealth > _healthMin)
        {
            _health = Mathf.Clamp(_health += amountHealth, _healthMin, _healthMax);
        }
    }

    public bool TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health -= damage, _healthMin, _healthMax);

        return _health <= 0;
    }
}
