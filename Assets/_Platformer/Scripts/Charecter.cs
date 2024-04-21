using System;
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
    [Space]
    [SerializeField] private CharacterAnimator animator;
    [Space]
    [SerializeField] private HealthDecorators decorators;

    private IHealth _health;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _health = health;

        DecorateHealth(decorators.flatDamage);
        DecorateHealth(decorators.damageCooldown);
    }

    public void DecorateHealth(HealthDecorator decorator)
    {
        _health = decorator.Assign(_health) ?? _health;
    }

    private void OnEnable()
    {
        health.onDeath += Death;
        health.onDamage += Damage;
    }

    private void OnDisable()
    {
        health.onDeath -= Death;
        health.onDamage -= Damage;
    }

    private void Update()
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
