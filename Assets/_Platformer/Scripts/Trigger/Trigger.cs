using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;


[Serializable]
public enum TriggerType
{
    Enter,
    Exit,
    Interactive
}


[RequireComponent(typeof(Collider2D))]
public abstract class Trigger : MonoBehaviour, IInteractive
{
    [Header("Trigger")]
    [SerializeField] private TriggerType type;
    [SerializeField] private int priority;
    [SerializeField] private string[] tags = { "Player" };
    [SerializeField] private bool once;

    private bool _done;

    public int Priorty => throw new NotImplementedException();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!tags.Contains(other.tag)) return;
        if (type != TriggerType.Enter || _done) return;
        _done = once;
        Activate(other);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (!tags.Contains(other.tag)) return;
        if (type != TriggerType.Exit || _done) return;
        _done = once;
        Activate(other);
    }

    public abstract void Activate(Collider2D other);

    public void Interact(Interactor interactor)
    {
        if (!CanInteractWith(interactor)) return;

        _done = once;
        Activate(interactor.GetComponent<Collider2D>());
    }

    public bool CanInteractWith(Interactor interactor)
    {
        return tags.Contains(interactor.tag)
            && type == TriggerType.Interactive
            && !_done;
    }
}

