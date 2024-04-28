using System;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    private Controls _controls;


    public Vector2 Move => _controls.Player.Move
        .ReadValue<Vector2>();


    public event Action onJump;
    public event Action onInteraction;
    public event Action onAttack;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Player.Jumping.performed += _ => onJump?.Invoke();
        _controls.Player.Interact.performed += _ => onInteraction?.Invoke();
        _controls.Player.Atack.performed += _ => onAttack?.Invoke();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
    private void OnDestroy()
    {
        _controls.Dispose();
    }
}

