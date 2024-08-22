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
    [SerializeField] private Health _healthEnemy;
    [SerializeField] private Coroutine _absorbing;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _enemyLayerMask;

    public event Action<float, float> ManaChanged;

    public void TryUseSkill()
    {
        if (GetEnemiesCollider2Ds().Length > 0 && _absorbing == null && _absorbMana == _absorbManaMax)
        {
            _absorbing = StartCoroutine(Absorbing());
        }
    }

    private Collider2D[] GetEnemiesCollider2Ds() => Physics2D.OverlapCircleAll(transform.position, _vampirismRadius, _enemyLayerMask);

    private void SetNearestEnemyHealth()
    {
        float sqrDistanceNearestEnemy = float.MaxValue;

        Collider2D nearestEnemyCollider = null;

        foreach (var enemyCollider in GetEnemiesCollider2Ds())
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
            _healthEnemy = healthEnemy;
        }
    }

    private IEnumerator Absorbing()
    {
        var wait = new WaitForEndOfFrame();

        while (GetEnemiesCollider2Ds().Length > 0 && _absorbMana > _absorbManaMin)
        {
            SetNearestEnemyHealth();

            _absorbMana -= _absorbManaMax * (Time.deltaTime / _absorbTime);

            ManaChanged?.Invoke(_absorbMana, _absorbManaMax);

            _healthPlayer.AddHealth(_healthEnemy.TakeDamage(_vampirismForce * Time.deltaTime));

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
            _absorbMana = Mathf.Clamp(_absorbMana + _absorbManaMax * (Time.deltaTime / _absorbReloadTime), _absorbManaMin, _absorbManaMax);

            ManaChanged?.Invoke(_absorbMana, _absorbManaMax);

            yield return wait;
        }
    }
}
