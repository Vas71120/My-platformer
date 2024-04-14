using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private string speedParameterName = "Speed";
    [SerializeField] private string VerticalSpeedParameterName = "VerticalSpeed";
    [SerializeField]
    private string isFallingParameterName
        = "IsFalling";
    [SerializeField] private string hurtTriggerName = "Hurt";


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetVelocity(Vector2 velocity)
    {
        _animator.SetFloat(speedParameterName, Mathf.Abs(velocity.x));
        _animator.SetFloat(VerticalSpeedParameterName, velocity.y);
    }


    public void SetIsFalling(bool isFalling)
    {
        _animator.SetBool(isFallingParameterName, isFalling);
    }


    public void Hurt()
    {
        _animator.SetTrigger(hurtTriggerName);
    }
}
