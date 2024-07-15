using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private readonly int VampirismAnimation = Animator.StringToHash(nameof(VampirismAnimation));

    [SerializeField] private float _vampirismForce = 5f;
    [SerializeField] private float _ababsorbTime = 6f;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private Health _healthPlayer;
    [SerializeField] private List<Health> _healthEnemies;
    [SerializeField] private Health _healthEnemy;
    [SerializeField] private bool _isEnemiInZone = false;
    [SerializeField] private float _distanceToEnemy ;
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isEnemiInZone = true;

        _healthEnemies.Add(collision.GetComponent<Health>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_healthEnemy == collision.GetComponent<Health>())
        {
            StopAllCoroutines();
        }
        
        _healthEnemies.Remove(collision.GetComponent<Health>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isEnemiInZone && _healthEnemies != null)
        {
            StopAllCoroutines();

            _distanceToEnemy = float.MaxValue; 
            
            foreach (var enemy in _healthEnemies)
            {
                var distanceToCurrentEnemy = (transform.position - enemy.transform.position).magnitude;

                if (_distanceToEnemy > distanceToCurrentEnemy)
                {
                    _distanceToEnemy = distanceToCurrentEnemy;
                    _healthEnemy = enemy.GetComponent<Health>();
                }
            }

            _isEnemiInZone = false;

            StartCoroutine(Absorbing());
        }
    }

    private IEnumerator Absorbing()
    {
        float absorptionTime = 0;

        var wait = new WaitForSeconds(_delay);

        while (absorptionTime < _ababsorbTime)
        {
            if (_healthEnemy != null)
            {
                _animator.Play(VampirismAnimation);

                _healthEnemy.TakeDamage(_vampirismForce);
                _healthPlayer.AddHealth(_vampirismForce);
            }

            absorptionTime += _delay;

            yield return wait;
        }

        _isEnemiInZone = true;
    }
}
