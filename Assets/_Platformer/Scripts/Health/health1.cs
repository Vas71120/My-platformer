using System;

public interface IHealth
{
    public float Max { get; }
    public float Ratio { get; }
    public bool IsAlive { get; }

    public event Action<IHealth, DamageInfo> onDamage;
    public event Action<IHealth, DamageInfo> onDeath;

    public bool CanBeDamaged(DamageInfo damageInfo);
    public float TakeDamage(DamageInfo damageInfo);
}