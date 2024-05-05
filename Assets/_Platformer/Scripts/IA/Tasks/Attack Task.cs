using System.Collections;
using MBT;
using UnityEngine;

public class AttackTask : Leaf
{
    [SerializeField] private AICharacterReference self = new(VarRefMode.DisableConstant);
    [SerializeField] private FloatReference attackRate = new(1f);
    private bool _isAttaking;
    public override NodeResult Execute()
    {
        if (_isAttaking) return NodeResult.running;

        self.Value.Weapon.Attack();
        StartCoroutine(DoCooldown(attackRate.Value));

        return NodeResult.success;
    }

    private IEnumerator DoCooldown(float duration)
    {
        _isAttaking = true;
        yield return new WaitForSeconds(duration);
        _isAttaking = false;
    }

}
