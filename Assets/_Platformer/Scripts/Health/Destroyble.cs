using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyble : MonoBehaviour, IDamagenable
{
    [SerializeField] private Health health;
    [SerializeField] private UnityEvent onDeath;
    private void OnEnable()
    {
        health.onDeath += Death;
    }
    private void OnDisable()
    {
        health.onDeath -= Death;
    }
    public void Death(IHealth component, DamageInfo damageInfo)
    {
        onDeath ?.Invoke();
    }
    public float TakeDamage(DamageInfo damageInfo)
    {
        return health.TakeDamage(damageInfo);
    }
}
