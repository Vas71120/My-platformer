using MBT;
using UnityEngine;
[AddComponentMenu("")]
[MBTNode("Tasks/AnimationBlendMode To")]

public class MoveTo : Leaf
{
    [SerializeField] private AICharacterReference self = new(VarRefMode.DisableConstant);
    [SerializeField] private Vector3Reference location = new(VarRefMode.DisableConstant);
    [SerializeField] private FloatReference threshold = new(0.5f);
    public override NodeResult Execute()
    {
        var position = self.Value.transform.position;
        var destination = location.Value;
        var direction = destination - position;
        self.Value.Walking.Move(direction.normalized);
        return direction.magnitude < threshold.Value
            ? NodeResult.success : NodeResult.running;
    }
}
