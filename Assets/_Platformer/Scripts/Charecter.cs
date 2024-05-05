using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IDamagenable
{
    [Serializable]
    private class HealthDecorators
    {
        [SerializeField] public FlatDamageDecorator flatDamage;
        [SerializeField] public DamageCooldownDecorator damageCooldown;
    }

    [SerializeField] private Health health;
    [SerializeField] private HealthDecorators decorators;
    [Space]
    [SerializeField] private CharacterAnimator animator;

    private IHealth _health;
    private Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        _health = health;

        DecorateHealth(decorators.flatDamage);
        DecorateHealth(decorators.damageCooldown);
    }

    public void DecorateHealth(HealthDecorator decorator)
    {
        _health = decorator.Assign(_health) ?? _health;
    }

    protected virtual void OnEnable()
    {
        health.onDeath += Death;
        health.onDamage += Damage;
    }

    protected virtual void OnDisable()
    {
        health.onDeath -= Death;
        health.onDamage -= Damage;
    }

    protected virtual void Update()
    {
        var rot = transform.eulerAngles;

        rot.y = _rigidbody.velocity.x switch
        {
            > 0 => 0f,
            < 0 => 180f,
            _ => rot.y
        };

        transform.eulerAngles = rot;
    }

    private void Death(IHealth component, DamageInfo damageInfo)
    {
        // TODO: Play anim, disable input
    }

    private void Damage(IHealth component, DamageInfo damageInfo)
    {
        animator.Hurt();
    }

    public float TakeDamage(DamageInfo damageInfo)
    {
        return _health.TakeDamage(damageInfo);
    }
}