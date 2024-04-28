using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputable
{
    public void SetupInput(InputManager inputManager);
    public void RemoveInput(InputManager inputManager);
}
