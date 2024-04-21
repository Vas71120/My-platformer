using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HealthDecorator : IHealth
{
    private IHealth _health;

    public virtual float Max => _health.Max;
    public virtual float Ratio => _health.Ratio;
    public virtual bool IsAlive => _health.IsAlive;

    public virtual event Action<IHealth, DamageInfo> onDamage
    {
        add => _health.onDamage += value;
        remove => _health.onDamage -= value;
    }

    public virtual event Action<IHealth, DamageInfo> onDeath
    {
        add => _health.onDeath += value;
        remove => _health.onDeath -= value;
    }

    public virtual bool CanBeDamaged(DamageInfo damageInfo)
    {
        return _health.CanBeDamaged(damageInfo);
    }

    public virtual float TakeDamage(DamageInfo damageInfo)
    {
        return _health.TakeDamage(damageInfo);
    }

    public virtual IHealth Assign(IHealth health)
    {
        _health = health;
        return this;
    }
}
