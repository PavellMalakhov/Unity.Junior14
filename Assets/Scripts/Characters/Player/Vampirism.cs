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
    [SerializeField] private float _sqrDistanceToEnemy;
    [SerializeField] private Health _healthPlayer;
    [SerializeField] private Health _healthEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Coroutine _absorbing;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Collider2D[] _enemyColliders2D;
    [SerializeField] private LayerMask _enemyLayerMask;

    public event Action<float, float> ManaChanged;

    public void TryUseSkill()
    {
        DefineEnemiesCollider2Ds();

        if (_enemyColliders2D.Length > 0 && _absorbing == null && _absorbMana == _absorbManaMax)
        {
            _absorbing = StartCoroutine(Absorbing());
        }
    }

    private void DefineEnemiesCollider2Ds()
    {
        _enemyColliders2D = Physics2D.OverlapCircleAll(transform.position, _vampirismRadius, _enemyLayerMask);
    }

    private IEnumerator Absorbing()
    {
        var wait = new WaitForEndOfFrame();

        while (_enemyColliders2D.Length > 0 && _absorbMana > _absorbManaMin)
        {
            SetNearestEnemyHealth();

            _absorbMana -= Time.deltaTime / _absorbTime;

            Absorb();

            yield return wait;
        }

        if (_absorbing != null)
        {
            StopCoroutine(_absorbing);
            _absorbing = null;
        }

        StartCoroutine(ReloadSkill());
    }

    private void SetNearestEnemyHealth()
    {
        _sqrDistanceToEnemy = float.MaxValue;

        foreach (var enemyCollider in _enemyColliders2D)
        {
            var sqrDistanceToCurrentEnemy = (transform.position - enemyCollider.transform.position).sqrMagnitude;

            if (sqrDistanceToCurrentEnemy < _sqrDistanceToEnemy)
            {
                _sqrDistanceToEnemy = sqrDistanceToCurrentEnemy;
                _healthEnemy = enemyCollider.GetComponent<Health>();
            }
        }
    }
    
    private void Absorb()
    {
        _healthPlayer.AddHealth(_healthEnemy.TakeDamage(_vampirismForce * Time.deltaTime));

        DefineEnemiesCollider2Ds();

        ManaChanged?.Invoke(_absorbMana, _absorbManaMax);
    }

    private IEnumerator ReloadSkill()
    {
        var wait = new WaitForEndOfFrame();

        while (_absorbMana != 1)
        {
            _absorbMana = Mathf.Clamp(_absorbMana + Time.deltaTime / _absorbReloadTime, _absorbManaMin, _absorbManaMax);

            ManaChanged?.Invoke(_absorbMana, _absorbManaMax);

            yield return wait;
        }
    }
}
