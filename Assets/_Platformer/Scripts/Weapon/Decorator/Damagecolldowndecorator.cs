using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DamageCooldownDecorator : HealthDecorator
{
    [SerializeField] private float cooldownTime = 1f;

    private bool _isInCooldown;

    public override IHealth Assign(IHealth health)
    {
        health.onDamage += OnDamage;
        return base.Assign(health);
    }

    public override bool CanBeDamaged(DamageInfo damageInfo)
    {
        return !_isInCooldown && base.CanBeDamaged(damageInfo);
    }

    public override float TakeDamage(DamageInfo damageInfo)
    {
        return _isInCooldown ? 0f : base.TakeDamage(damageInfo);
    }

    private void OnDamage(IHealth comp, DamageInfo damageInfo)
    {
        CoroutineRunner.instance.StartCoroutine(DoCooldown());
    }

    private IEnumerator DoCooldown()
    {
        _isInCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        _isInCooldown = false;
    }
}

