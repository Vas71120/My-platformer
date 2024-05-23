using System;
using UnityEngine;


[Serializable]
public class FlatDamageDecorator : HealthDecorator
{
    [SerializeField] public float maxDamage = 20f;


    public override float TakeDamage(DamageInfo damage)
    {
        damage.amount = Mathf.Min(maxDamage, damage.amount);
        return base.TakeDamage(damage);
    }
}
