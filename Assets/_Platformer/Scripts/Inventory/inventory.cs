using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    [Serializable]
    public class ItemSlot
    {
        public item item;
        public int amount;
        public bool IsFull => amount >= item.MaxAmount;
    }
    [SerializeField, Min(0)] private int capacity = 5;
    [SerializeField] private List<ItemSlot> items;
    public delegate void OnItemCollected(item item, int amount);
    public event OnItemCollected onItemCollected;
    public bool CanPickup(item item)
    {
        bool hasStored = items.Find(x => x.item.Equals(item) && !x.IsFull) != null;
        bool hasSpase = items.Count + 1 <= capacity;
        return hasStored || hasSpase;
    }
    public int Pickup(item item, int amount) 
    {
        if (!item || amount == 0) return 0;
        var startAmount = amount;
        var stored = items.Find(x => x.item.Equals(item) && !x.IsFull);
        if (stored !=null)
        {
            var oldAmount = stored.amount;
            var left = stored.item.MaxAmount - stored.amount;
            var delta = stored.amount - oldAmount;
            amount -= delta;
            onItemCollected?.Invoke(item, delta);
        }
        while(amount > 0 && CanPickup(item))
        {
            var delta = Math.Min(amount, item.MaxAmount);
            items.Add(new ItemSlot
            {
                item = item,
                amount = delta
            });
            amount -= delta;
            onItemCollected?.Invoke(item, delta);
        }
        return Math.Abs(startAmount - amount);
    }
}
