using UnityEngine;

public abstract class EventHandler : Attribute
{
    [SerializeField] protected Health _health;

    protected void OnEnable()
    {
        _health.Changed += RenderHealh;
    }

    protected void OnDisable()
    {
        _health.Changed -= RenderHealh;
    }

    protected virtual void RenderHealh(float health, float healthMax) { }
}