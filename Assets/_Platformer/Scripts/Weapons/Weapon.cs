using System;
using System.Collections;
using UnityEngine;

[Serializable]
public struct AttackInfo
{
    [SerializeField] public DamageInfo damage;
    [SerializeField, Min(0f)] public float range;
    [SerializeField, Min(0f)] public float preporation;
    [SerializeField, Min(0f)] public float duration;
    [SerializeField, Min(0f)] public float cooldown;
}
public enum MeleeStste
{
    Idle,
    Preporation,
    Execution,
    Cooldown
}
public class Weapon : MonoBehaviour, IInputable
{
    [Header("Melee")]
    [SerializeField] private MeleeConfig config;
    [SerializeField] private Transform attackOffset;
    [SerializeField] private LayerMask layers;
    private bool _pendingAttack;
    private int _comboCounter;
    private MeleeStste _state;
    public int ComboCounter
    {
        get => _comboCounter;
        private set => _comboCounter = value % config.Attacks.Count;
    }
    public bool IsAttacking => _state != MeleeStste.Idle;
    public event Action<Weapon> onAtack;
    public void Attack()
    {
        if (_pendingAttack) return;
        if (_state > MeleeStste.Idle)
        {
            _pendingAttack = true;
            return;
        }
        StartCoroutine(PerformAttack());
    }
    private IEnumerator PerformAttack()
    {
        if (IsAttacking) yield break;
        var attack = config.Attacks[ComboCounter];
        _state = MeleeStste.Preporation;
        onAtack?.Invoke(this);
        yield return new WaitForSeconds(attack.preporation);
        _pendingAttack = false;
        _state = MeleeStste.Execution;
        StartCoroutine(DealDamage(attack));
        yield return new WaitForSeconds(attack.duration);
        _state = MeleeStste.Cooldown;
        yield return new WaitForSeconds(attack.cooldown); 
        ComboCounter++;
        _state = MeleeStste.Idle;
        if (_pendingAttack)
        {
            StartCoroutine(PerformAttack());
            yield break;
        }
        yield return new WaitForSeconds(config.ComboCooldown);
        if (!IsAttacking) ComboCounter = 0;
    }
    private IEnumerator DealDamage(AttackInfo attack)
    {
        while (_state == MeleeStste.Execution)
        {
            var results = Physics2D.CircleCastAll(attackOffset.position, attack.range,
                transform.right, 0f, layers);
            foreach (var result in results)
            {
                var damageble = result.collider.GetComponent<IDamagenable>();
                damageble?.TakeDamage(attack.damage);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    public void SetupInput(InputManager inputManager)
    {
        if (!inputManager) return;
        inputManager.onAttack += Attack;
    }
    public void RemoveInput(InputManager inputManager)
    {
        if (!inputManager) return;
        inputManager.onAttack -= Attack;
    }

}
