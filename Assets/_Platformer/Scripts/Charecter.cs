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

    private IList<IInputable> _inputables;

    private InputManager _inputManager;

    public InputManager InputManager
    {
        get => _inputManager;
        set
        {
            if (_inputManager)
                foreach (var inputable in _inputables)
                    inputable.RemoveInput(_inputManager);

            _inputManager = value;

            if (_inputManager)
                foreach (var inputable in _inputables)
                    inputable.SetupInput(_inputManager);
        }
    }

    private void InitInputManager()
    {
        if (_inputManager) return;

        var go = new GameObject("InputManger");
        go.transform.SetParent(gameObject.transform);
        InputManager = go.AddComponent<InputManager>();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _inputables = GetComponents<IInputable>()
                .Concat(GetComponentsInChildren<IInputable>())
                .ToList();
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

        InitInputManager();
    }

    private void OnDisable()
    {
        health.onDeath -= Death;
        health.onDamage -= Damage;

        Destroy(InputManager.gameObject);
        InputManager = null;
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