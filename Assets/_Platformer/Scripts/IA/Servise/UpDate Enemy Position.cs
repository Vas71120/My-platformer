using MBT;
using UnityEngine;

public class UpDateEnemyPosition : Service
{
    [SerializeField] private GameObjectReference enemy = new(VarRefMode.DisableConstant);
    [SerializeField] private Vector3Reference location = new(VarRefMode.DisableConstant);
    public override void Task()
    {
        if (!enemy.Value) return;
        location.Value = enemy.Value.transform.position;
    }
}
