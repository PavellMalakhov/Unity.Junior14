using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vampirism : MonoBehaviour
{
    private readonly int _vampirismAnimation = Animator.StringToHash(nameof(_vampirismAnimation));

    [SerializeField] private Slider _skillBar;
    [SerializeField] private float _vampirismForce = 5f;
    [SerializeField] private float _absorbTime = 6f;
    [SerializeField] private float _absorbReloadTime = 6f;
    [SerializeField] private float _delay = 1;
    [SerializeField] private Health _healthPlayer;
    [SerializeField] private List<Health> _healthEnemies;
    [SerializeField] private Health _healthEnemy;
    [SerializeField] private bool _isEnemiInZone = false;
    [SerializeField] private float _sqrDistanceToEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Coroutine _absorbing;
    [SerializeField] private Coroutine _absorbingTime;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Collider2D[] _enemyColliders2D;
    [SerializeField] private LayerMask _enemyLayerMask;

    private Dictionary<Collider2D, Health> _enemiesCollider2DHealth = new();
    private float _gameZoneSize = 1000f;

    private void Start()
    {
        _enemyColliders2D = Physics2D.OverlapCircleAll(transform.position, _gameZoneSize, _enemyLayerMask);

        if (_enemyColliders2D != null)
        {
            for (int i = 0; i < _enemyColliders2D.Length; i++)
            {
                _enemiesCollider2DHealth.Add(_enemyColliders2D[i], _enemyColliders2D[i].GetComponent<Health>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _healthEnemies.Add(_enemiesCollider2DHealth[collision]);

        _isEnemiInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_healthEnemy == _enemiesCollider2DHealth[collision] && _absorbingTime != null)
        {
            StopCoroutines();
        }

        _healthEnemies.Remove(_enemiesCollider2DHealth[collision]);

        if (_healthEnemy == _enemiesCollider2DHealth[collision])
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
            _sqrDistanceToEnemy = float.MaxValue;

            foreach (var enemy in _healthEnemies)
            {
                var sqrDistanceToCurrentEnemy = (transform.position - enemy.transform.position).sqrMagnitude;

                if (sqrDistanceToCurrentEnemy < _sqrDistanceToEnemy)
                {
                    if (_healthEnemy != enemy)
                    {
                        StopCoroutines();
                    }

                    _sqrDistanceToEnemy = sqrDistanceToCurrentEnemy;
                    _healthEnemy = enemy;
                }
            }
        }

        if (_inputReader.GetUseSkillk() && _isEnemiInZone && _absorbingTime == null && _skillBar.value == 1)
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

            _skillBar.value -= Time.deltaTime / _absorbTime;

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

        StopCoroutines();
        StartCoroutine(ReloadSkill());
    }

    private IEnumerator ReloadSkill()
    {
        var wait = new WaitForEndOfFrame();

        while (_skillBar.value != 1)
        {
            _skillBar.value += Time.deltaTime / _absorbReloadTime;

            yield return wait;
        }
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

        StartCoroutine(ReloadSkill());
    }
}
