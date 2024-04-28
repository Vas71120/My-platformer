using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee", menuName = "Weapon/Melee")]
public class MeleeConfig : ScriptableObject
{
    [SerializeField] private AttackInfo[] attacks;
    [SerializeField, Min(0f)] private float comboCooldown;
    public IReadOnlyList<AttackInfo> Attacks => attacks;
    public float ComboCooldown => comboCooldown;
}
