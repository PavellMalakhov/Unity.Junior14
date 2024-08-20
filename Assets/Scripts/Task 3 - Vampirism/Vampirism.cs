using System.Collections;
using UnityEngine;

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
    [SerializeField] private VampirismVisualization _vampirismVisualization;

    public bool UseSkill()
    {
        _enemyColliders2D = GetEnemiesCollider2Ds();

        if (_enemyColliders2D != null && _absorbing == null && _absorbMana == _absorbManaMax)
        {
            _absorbing = StartCoroutine(Absorbing());

            _vampirismVisualization.StartAnimation(_absorbTime);

            return true;
        }

        return false;
    }

    private Collider2D[] GetEnemiesCollider2Ds() => Physics2D.OverlapCircleAll(transform.position, _vampirismRadius, _enemyLayerMask);

    private void Absorb()
    {
        _healthPlayer.AddHealth(_healthEnemy.TakeDamage(_vampirismForce * Time.deltaTime));

        _enemyColliders2D = GetEnemiesCollider2Ds();
    }

    private IEnumerator Absorbing()
    {
        var wait = new WaitForEndOfFrame();

        while (_enemyColliders2D.Length == 1 && _absorbMana > _absorbManaMin)
        {
            _healthEnemy = _enemyColliders2D[0].GetComponent<Health>();

            Absorb();

            _absorbMana -= Time.deltaTime / _absorbTime;

            _vampirismVisualization.SetSkillBar(_absorbMana);

            yield return wait;
        }

        while (_enemyColliders2D.Length > 1 && _absorbMana > _absorbManaMin)
        {
            _sqrDistanceToEnemy = float.MaxValue;

            foreach (var enemyCollider in _enemyColliders2D)
            {
                var sqrDistanceToCurrentEnemy = (transform.position - enemyCollider.transform.position).sqrMagnitude;

                if (sqrDistanceToCurrentEnemy < _sqrDistanceToEnemy)
                {
                    _sqrDistanceToEnemy = sqrDistanceToCurrentEnemy;
                    _healthEnemy = enemyCollider.GetComponent<Health>();

                    Absorb();

                    _absorbMana -= Time.deltaTime / _absorbTime;

                    _vampirismVisualization.SetSkillBar(_absorbMana);

                    yield return wait;
                }
            }
        }

        if (_absorbing != null)
        {
            _vampirismVisualization.StopAnimation();

            StopCoroutine(_absorbing);
            _absorbing = null;
        }

        StartCoroutine(ReloadSkill());
    }

    private IEnumerator ReloadSkill()
    {
        var wait = new WaitForEndOfFrame();

        while (_absorbMana != 1)
        {
            _absorbMana = Mathf.Clamp(_absorbMana + Time.deltaTime / _absorbReloadTime, _absorbManaMin, _absorbManaMax);

            _vampirismVisualization.SetSkillBar(_absorbMana);

            yield return wait;
        }
    }
}
