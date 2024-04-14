using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColectableItem : MonoBehaviour
{
    [SerializeField] private item item;
    [SerializeField, Min(0)] private int amount = 1;
    [Space]
    [SerializeField] private UnityEvent onPickUp;
    private void OnValidate()
    {
        if (!item) return;
        GetComponent<SpriteRenderer>().sprite = item.Sprite;
        amount = Mathf.Min(amount, 0, item.MaxAmount);
    }
    public void PickUp(inventory inventory)
    {
        if (!inventory) return;
        amount -= inventory.Pickup(item, amount);
        if (amount <= 0)
        {
            onPickUp?.Invoke();
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUp(collision.GetComponent<inventory>());
    }
}
