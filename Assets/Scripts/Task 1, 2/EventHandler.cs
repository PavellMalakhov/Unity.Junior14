using UnityEngine;

public abstract class EventHandler : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected void OnEnable()
    {
        _health.HealthChanged += RenderHealh;
    }

    protected void OnDisable()
    {
        _health.HealthChanged -= RenderHealh;
    }

    protected virtual void RenderHealh(float health, float healthMax) { }
}