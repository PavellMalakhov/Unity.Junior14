using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    public void AddHealth()
    {
        _health += 100f;
    }

    public bool TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
