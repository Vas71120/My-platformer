using UnityEngine;
public class JumpTrigger : Trigger
{
    [SerializeField] private float force = 5f;
    public override void Activate(Collider2D other)
    {
        var velocity = other.attachedRigidbody.velocity;
        velocity.y += force;
        other.attachedRigidbody.velocity = velocity;
    }
}
