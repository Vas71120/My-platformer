using System.Collections;
using System.Collections.Generic;
using MBT;
using UnityEngine;

public class IsCloseCondition : Condition
{
    [SerializeField] private Abort abort;
    [SerializeField] private AICharacterReference self = new(VarRefMode.DisableConstant);
    [SerializeField] private Vector3Reference location = new(VarRefMode.DisableConstant);
    [SerializeField] private FloatReference threeshold = new(10f);
    [SerializeField] private bool invert;
    public override bool Check()
    {
        var sqrMagnitude = (self.Value.transform.position - location.Value).sqrMagnitude;
        var dist = threeshold.Value;
        return (sqrMagnitude < dist * dist) ^ invert;
    }
    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            location.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            location.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(Vector3 oldValue, Vector3 newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
   
    public override bool IsValid()
    {
        return !location.isInvalid;
    }
    
}
