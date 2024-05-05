using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacter : Character
{
    private IList<IInputable> _inputables;

    private InputManager _inputManager;

    public InputManager InputManager
    {
        get => _inputManager;
        set
        {
            if (_inputManager)
                foreach (var inputable in _inputables)
                    inputable.RemoveInput(_inputManager);

            _inputManager = value;

            if (_inputManager)
                foreach (var inputable in _inputables)
                    inputable.SetupInput(_inputManager);
        }
    }

    private void InitInputManager()
    {
        if (_inputManager) return;

        var go = new GameObject("InputManger");
        go.transform.SetParent(transform.parent);
        InputManager = go.AddComponent<InputManager>();
    }

    protected override void Awake()
    {
        base.Awake();
        _inputables = GetComponents<IInputable>()
                .Concat(GetComponentsInChildren<IInputable>())
                .ToList();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InitInputManager();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Destroy(InputManager.gameObject);
        InputManager = null;
    }
}