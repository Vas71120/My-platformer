using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour, IInputable
{
    public void SetupInput(InputManager inputManager)
    {
        if (!inputManager) return;
        inputManager.onInteraction += Interact;
    }
    public void RemoveInput(InputManager inputManager)
    {
        if (!inputManager) return;
        inputManager.onInteraction -= Interact;
    }
    private List<IInteractive> _interactiveObjects = new();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactive = collision.GetComponent<IInteractive>();
        if (interactive != null) _interactiveObjects.Add(interactive);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactive = collision.GetComponent<IInteractive>();
        if (interactive != null) _interactiveObjects.Remove(interactive);
    }
    private void Sort()
    {
        if (!HasInteractions()) return;
        _interactiveObjects = _interactiveObjects.OrderBy(x => x.Priorty).ToList();
    }
    public void Interact() 
    {
        Sort();
        var interactive = _interactiveObjects.LastOrDefault(x => x.CanInteractWith(this));
        interactive?.Interact(this);
    }
    public bool HasInteractions()
    {
        return _interactiveObjects.Count(x => x.CanInteractWith(this)) > 0;
    }

}
