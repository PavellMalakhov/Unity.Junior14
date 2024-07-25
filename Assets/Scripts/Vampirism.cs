using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private readonly int _vampirismAnimation = Animator.StringToHash(nameof(_vampirismAnimation));

    [SerializeField] private float _vampirismForce = 5f;
    [SerializeField] private float _absorbTime = 6;
    [SerializeField] private float _delay = 1;
    [SerializeField] private Health _healthPlayer;
    [SerializeField] private List<Health> _healthEnemies;
    [SerializeField] private Health _healthEnemy;
    [SerializeField] private bool _isEnemiInZone = false;
    [SerializeField] private float _distanceToEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Coroutine _absorbing;
    [SerializeField] private Coroutine _absorbingTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _healthEnemies.Add(collision.GetComponent<Health>());

        _isEnemiInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_healthEnemy == collision.GetComponent<Health>() && _absorbingTime != null)
        {
            StopCoroutines();
        }

        _healthEnemies.Remove(collision.GetComponent<Health>());

        if (_healthEnemy == collision.GetComponent<Health>())
        {
            _healthEnemy = null;
        }

        if (_healthEnemies.Count == 0)
        {
            _isEnemiInZone = false;
        }
    }

    private void Update()
    {
        if (_isEnemiInZone && _healthEnemies.Count == 1 && _healthEnemy != _healthEnemies[0])
        {
            _healthEnemy = _healthEnemies[0];
        }
        
        if (_isEnemiInZone && _healthEnemies.Count > 1)
        {
            _distanceToEnemy = float.MaxValue;

            foreach (var enemy in _healthEnemies)
            {
                var distanceToCurrentEnemy = (transform.position - enemy.transform.position).magnitude;

                if (distanceToCurrentEnemy < _distanceToEnemy)
                {
                    if (_healthEnemy != enemy.GetComponent<Health>())
                    {
                        StopCoroutines();
                    }

                    _distanceToEnemy = distanceToCurrentEnemy;
                    _healthEnemy = enemy.GetComponent<Health>();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && _isEnemiInZone && _absorbingTime == null)
        {
            _absorbing = StartCoroutine(Absorbing());
            _absorbingTime = StartCoroutine(AbsorbingTime());
        }
    }

    private IEnumerator Absorbing()
    {
        var wait = new WaitForEndOfFrame();

        while (enabled)
        {
            _healthEnemy.TakeDamage(_vampirismForce * Time.deltaTime);
            _healthPlayer.AddHealth(_vampirismForce * Time.deltaTime);

            yield return wait;
        }
    }

    private IEnumerator AbsorbingTime()
    {
        var wait = new WaitForSeconds(_delay);

        for (int i = 0; i < _absorbTime; i++)
        {
            _animator.Play(_vampirismAnimation);

            yield return wait;
        }

        StopCoroutine(_absorbing);
    }

    private void StopCoroutines()
    {
        if (_absorbing != null)
        {
            StopCoroutine(_absorbing);
            _absorbing = null;
        }

        if (_absorbingTime != null)
        {
            StopCoroutine(_absorbingTime);
            _absorbingTime = null;
        }
    }
}
