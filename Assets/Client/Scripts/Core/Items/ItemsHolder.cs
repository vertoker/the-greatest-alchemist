using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    public class ItemsHolder : MonoBehaviour
    {
        [SerializeField] private Item[] _items;
        public static Dictionary<int, Item> Items { get; private set; } = new Dictionary<int, Item>();

        private void Awake()
        {
            foreach (var item in _items)
            {
                Items.Add(item.ID, item);
            }
        }
    }
}