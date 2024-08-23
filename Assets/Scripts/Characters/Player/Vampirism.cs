using System.Collections;
using UnityEngine;
using System;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _vampirismForce = 5f;
    [SerializeField] private float _vampirismRadius = 6f;
    [SerializeField] private float _absorbTime = 6f;
    [SerializeField] private float _absorbReloadTime = 6f;
    [SerializeField] private float _absorbMana = 1f;
    [SerializeField] private float _absorbManaMax = 1f;
    [SerializeField] private float _absorbManaMin = 0f;
    [SerializeField] private Health _healthPlayer;
    [SerializeField] private Coroutine _absorbing;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _enemyLayerMask;

    public event Action<float, float> ManaChanged;

    public void TryUseSkill()
    {
        Collider2D[] enemiesCollider2Ds = GetEnemiesCollider2Ds();

        if (enemiesCollider2Ds.Length > 0 && _absorbing == null && _absorbMana == _absorbManaMax)
        {
            _absorbing = StartCoroutine(Absorbing(enemiesCollider2Ds));
        }
    }

    private Collider2D[] GetEnemiesCollider2Ds()
    {
        return Physics2D.OverlapCircleAll(transform.position, _vampirismRadius, _enemyLayerMask);
    }

    private Health GetNearestEnemyHealth(Collider2D[] enemiesCollider2Ds)
    {
        float sqrDistanceNearestEnemy = float.MaxValue;

        Collider2D nearestEnemyCollider = null;

        foreach (var enemyCollider in enemiesCollider2Ds)
        {
            float sqrDistanceToEnemyCollider = (transform.position - enemyCollider.transform.position).sqrMagnitude;

            if (sqrDistanceToEnemyCollider < sqrDistanceNearestEnemy)
            {
                sqrDistanceNearestEnemy = sqrDistanceToEnemyCollider;

                nearestEnemyCollider = enemyCollider;
            }
        }

        if (nearestEnemyCollider.TryGetComponent<Health>(out Health healthEnemy))
        {
            return healthEnemy;
        }

        return null;
    }

    private IEnumerator Absorbing(Collider2D[] enemiesCollider2Ds)
    {
        var wait = new WaitForEndOfFrame();

        while (enemiesCollider2Ds.Length > 0 && _absorbMana > _absorbManaMin)
        {
            _absorbMana -= _absorbManaMax * (Time.deltaTime / _absorbTime);

            ManaChanged?.Invoke(_absorbMana, _absorbManaMax);

            _healthPlayer.AddHealth(GetNearestEnemyHealth(enemiesCollider2Ds).TakeDamage(_vampirismForce * Time.deltaTime));

            enemiesCollider2Ds = GetEnemiesCollider2Ds();

            yield return wait;
        }

        if (_absorbing != null)
        {
            StopCoroutine(_absorbing);

            _absorbing = null;
        }

        StartCoroutine(ReloadSkill());
    }

    private IEnumerator ReloadSkill()
    {
        var wait = new WaitForEndOfFrame();

        while (_absorbMana != _absorbManaMax)
        {
            float manaIncrement = _absorbManaMax * (Time.deltaTime / _absorbReloadTime);

            _absorbMana = Mathf.Clamp(_absorbMana + manaIncrement, _absorbManaMin, _absorbManaMax);

            ManaChanged?.Invoke(_absorbMana, _absorbManaMax);

            yield return wait;
        }
    }
}
