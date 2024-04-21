using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Trigger
{
    [Header("Damage")]
    [SerializeField] private DamageInfo damage;
    public override void Activate(Collider2D other)
    {
        var damagable = other.GetComponent<IDamagenable>();
        damagable?.TakeDamage(damage);
    }

}
