using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    public struct InvertoryItem
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _capacity;

        public bool CanFullAdd(int capacity)
        {
            return !(_capacity + capacity > _item.MaxCapacity);
        }
        public bool CanFullRemove(int capacity)
        {
            return !(_capacity < capacity);
        }
    }
}