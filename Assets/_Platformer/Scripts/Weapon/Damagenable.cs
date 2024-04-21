using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Damagenable : MonoBehaviour
{
    public float damage;
}
public interface IDamagenable
{
    public float TakeDamage(DamageInfo damageInfo);
}