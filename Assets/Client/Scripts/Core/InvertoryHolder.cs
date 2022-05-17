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
        [SerializeField] private int _invertoryCapacity = 100;
        [SerializeField] private InvertoryItem[] _items;
        [SerializeField] private InvertoryController _controller;

        private void Awake()
        {
            _items = new InvertoryItem[_invertoryCapacity];
        }

        public void Add(Item item, int quantity)
        {
            for (int i = 0; i < _items.Length; i++)
                if (!_items[i].IsEmpty())// ���� ���� �������
                    if (_items[i].Item.ID == item.ID)// ���� �� ��������� � ��� ���������
                        if (_items[i].FullAdd(ref quantity))// ��������� ���������� � ��������
                            break;// ���� ���������� ���������

            if (quantity > 0)// ���� ���������� �� ���������
            {
                int whole = quantity / item.Capacity;// ������� ������ ���� ��������
                int remainder = quantity - whole * item.Capacity;// ������� � ����� ���������
                for (int i = 0; i < _items.Length; i++)
                {
                    if (_items[i].IsEmpty())// ���� ���� ��������
                    {
                        if (whole == 0)// ���� ��������� �����
                        {
                            if (remainder > 0)
                                _items[i].Add(item, remainder);
                            break;
                        }

                        _items[i].Add(item, item.Capacity);
                        whole--;
                    }
                }
            }

            _controller.UpdateUI(_items);
        }
        public bool Remove(Item item, int quantity)
        {
            int capacityItemAll = 0;
            List<int> allItemsID = new List<int>();
            for (int i = 0; i < _items.Length; i++)
            {
                if (!_items[i].IsEmpty())// ���� ���� �������
                {
                    if (_items[i].Item.ID == item.ID)// ���� �� ��������� � ��� ���������
                    {
                        allItemsID.Add(i);
                        capacityItemAll += _items[i].Quantity;
                    }
                }
            }

            if (quantity > capacityItemAll)
                return false;
            
            for (int i = 0; i < allItemsID.Count; i++)
            {
                if (_items[allItemsID[i]].FullRemove(ref quantity))
                    break;
                else
                    _items[allItemsID[i]].Remove();
            }

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