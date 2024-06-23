using UnityEngine;

public class Combat : MonoBehaviour
{
    private Enemy _enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemy = enemy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemy = null;
        }
    }

    public void Attack(float forceAttack)
    {
        if (_enemy != null)
        {
            _enemy.TakeDamage(forceAttack);
        }
    }
}
