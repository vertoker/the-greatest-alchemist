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
    }
}