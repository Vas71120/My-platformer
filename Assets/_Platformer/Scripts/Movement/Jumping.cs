using UnityEngine;


public class Jumping : MonoBehaviour, IInputable
{
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps;
    [SerializeField, Range(0f, 5f)]
    private float
                                 downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)]
    private float
                                 upwardMovementMultiplier = 1.7f;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Ground ground;


    private Vector2 _velocity;


    private int _jumpPhase;
    private float _defaultGravityScale;
    private float _jumpSpeed;


    private bool _desiredJump;
    private bool _onGround;


    private void Start()
    {
        _defaultGravityScale = 1f;
    }


    public void SetupInput(InputManager inputManager)
    {
        if (!inputManager) return;
        inputManager.onJump +=Jump;
    }


    public void RemoveInput(InputManager inputManager)
    {
        if (!inputManager) return;
        inputManager.onJump -= Jump;
    }


    public void Jump()
    {
        _desiredJump = true;
    }
    private void DoJump()
    {
        if (_onGround || _jumpPhase < maxAirJumps)
        {
            _jumpPhase += 1;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y
                                          * jumpHeight);
            if (_velocity.y > 0)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(body.velocity.y);
            }
            _velocity.y += _jumpSpeed;
        }
    }
    private void FixedUpdate()
    {
        _onGround = ground.OnGround;
        _velocity = body.velocity;
        if (_onGround)
        {
            _jumpPhase = 0;
        }


        if (_desiredJump)
        {
            _desiredJump = false;
            DoJump();
        }
        if (body.velocity.y > 0f)
        {
            body.gravityScale = upwardMovementMultiplier;
        }
        else if (body.velocity.y < 0f)
        {
            body.gravityScale = downwardMovementMultiplier;
        }
        else if (body.velocity.y == 0f)
        {
            body.gravityScale = _defaultGravityScale;
        }
        body.velocity = _velocity;
    }
}

