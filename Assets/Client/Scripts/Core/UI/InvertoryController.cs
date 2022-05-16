using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.Items;
using Game.Pool;

namespace Core.UI
{
    public class InvertoryController : MonoBehaviour
    {
        private List<ItemSlot> slots = new List<ItemSlot>();
        private RectTransform _transform;
        private PoolSpawner _spawner;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _spawner = GetComponent<PoolSpawner>();
        }

        public void UpdateUI(List<InvertoryItem> items)
        {

        }

        public void Add(Item item, int quantity)
        {
            var slot = _spawner.Dequeue().GetComponent<ItemSlot>();
            slot.SetItem(item);
            slots.Add(slot);
        }
        public void Remove(Item item, int quantity)
        {

        }
    }
}