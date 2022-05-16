using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Core.Items;
using Core.UI;

namespace Core
{
    public class InvertoryHolder : MonoBehaviour
    {
        [SerializeField] private List<InvertoryItem> _items = new List<InvertoryItem>();
        [SerializeField] private InvertoryController _controller;

        public void Add(Item item, int quantity)
        {
            for (int i = 0; i < _items.Count; i++)
                if (_items[i].Item.ID == item.ID)
                    if (_items[i].FullAdd(ref quantity))
                        break;

            if (quantity > 0)
            {
                int whole = quantity / item.Capacity;
                int remainder = quantity - whole * item.Capacity;
                for (int i = 0; i < whole; i++)
                    _items.Add(new InvertoryItem() { Item = item, Quantity = item.Capacity });
                if (remainder > 0)
                    _items.Add(new InvertoryItem() { Item = item, Quantity = remainder });
            }

            _controller.UpdateUI(_items);
        }
        public bool Remove(Item item, int quantity)//Must fix
        {
            int capacityItemAll = 0;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Item.ID == item.ID)
                {
                    capacityItemAll += _items[i].Quantity;
                }
            }

            if (quantity > capacityItemAll)
                return false;
            
            List<int> deleteItems = new List<int>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Item.ID == item.ID)
                {
                    if (_items[i].FullRemove(ref quantity))
                    {
                        break;
                    }
                    else
                    {
                        deleteItems.Add(i);
                    }
                }
            }

            for (int i = deleteItems.Count - 1; i >= 0; i--)
                _items.RemoveAt(deleteItems[i]);

            return true;
        }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(InvertoryHolder))]
    public class InvertoryHolderEditor : Editor
    {
        string itemID, quantity;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var holder = (InvertoryHolder)target;

            GUILayout.Space(15);
            itemID = EditorGUILayout.TextField(itemID);
            quantity = EditorGUILayout.TextField(quantity);

            if (GUILayout.Button("Add Item"))
            {
                holder.Add(ItemsHolder.Items[int.Parse(itemID)], int.Parse(quantity));
            }
            if (GUILayout.Button("Remove Item"))
            {
                holder.Remove(ItemsHolder.Items[int.Parse(itemID)], int.Parse(quantity));
            }
        }
    }
#endif
}