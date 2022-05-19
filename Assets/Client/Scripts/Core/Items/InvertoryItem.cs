using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    [System.Serializable]
    public class InvertoryItem
    {
        public Item Item;
        public int Quantity;

        public void Add(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
        public void Remove()
        {
            Item = null;
        }
        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool FullAdd(ref int quantity)
        {
            if (Quantity == Item.Capacity)
                return false;
            if (Quantity + quantity > Item.Capacity)
            {
                quantity -= Item.Capacity - Quantity;
                Quantity = Item.Capacity;
                return false;
            }
            Quantity += quantity;
            quantity = 0;
            return true;
        }
        public bool FullRemove(ref int quantity)
        {
            if (Quantity == quantity)
            {
                Quantity = 0;
                quantity = 0;
                return false;
            }
            if (Quantity < quantity)
            {
                quantity -= Quantity;
                Quantity = 0;
                return false;
            }
            Quantity -= quantity;
            quantity = 0;
            return true;
        }

        public static bool EqualsItem(InvertoryItem item1, InvertoryItem item2)
        {
            if (!item1.IsEmpty() && !item2.IsEmpty())
                return item1.Item.ID == item2.Item.ID;
            return false;
        }
    }
}