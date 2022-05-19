using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Game.Pool;
using Core.Items;
using Core.UI;

namespace Core
{
    public class InvertoryHolder : MonoBehaviour
    {
        private int _invertoryCapacity;
        [SerializeField] private InvertoryItem[] _items;
        [SerializeField] private InvertoryController _controller;
        [SerializeField] private PoolData _dataCapacity;

        private void Awake()
        {
            _invertoryCapacity = _dataCapacity.GetCapacity;
            _items = new InvertoryItem[_invertoryCapacity];
            for (int i = 0; i < _invertoryCapacity; i++)
                _items[i] = new InvertoryItem();

            _controller.Init(Switch, Collect, AppendDrag, StartDrag, FinishDrag);
        }

        public void Add(Item item, int quantity, bool inStackable = true)
        {
            if (inStackable)
                for (int i = 0; i < _items.Length; i++)
                    if (!_items[i].IsEmpty())// Если есть предмет
                        if (_items[i].Item.ID == item.ID)// Если он совпадает с тем предметом
                            if (_items[i].FullAdd(ref quantity))// Добавляет количество к предмету
                                break;// Если количество кончилось

            if (quantity > 0)// Если количество не кончилось
            {
                int whole = quantity / item.Capacity;// Сколько стаков надо добавить
                int remainder = quantity - whole * item.Capacity;// Сколько в итоге останется
                for (int i = 0; i < _invertoryCapacity; i++)
                {
                    if (_items[i].IsEmpty())// Если нету предмета
                    {
                        if (whole == 0)// Если кончились стаки
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
            for (int i = 0; i < _invertoryCapacity; i++)
            {
                if (!_items[i].IsEmpty())// Если есть предмет
                {
                    if (_items[i].Item.ID == item.ID)// Если он совпадает с тем предметом
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

            _controller.UpdateUI(_items);
            return true;
        }
        public void Switch(int id1, int id2)
        {
            if (InvertoryItem.EqualsItem(_items[id1], _items[id2]))
            {
                if (_items[id2].FullAdd(ref _items[id1].Quantity))
                    _items[id1].Remove();
            }
            else
            {
                InvertoryItem temp = _items[id1];
                _items[id1] = _items[id2];
                _items[id2] = temp;
            }

            _controller.UpdateUI(_items);
        }
        public void Collect(int id)
        {
            if (_items[id].IsEmpty())
                return;

            int quantity = _items[id].Item.Capacity - _items[id].Quantity;

            for (int i = 0; i < _invertoryCapacity; i++)
            {
                if (!_items[i].IsEmpty())// Если есть предмет
                {
                    if (_items[i].Item.ID == _items[id].Item.ID)// Если он совпадает с тем предметом
                    {
                        if (i != id)// Фильтр для слота
                        {
                            int quantityDependency = _items[i].Quantity;// Количество дочернего предмета
                            if (_items[i].FullRemove(ref quantity))// Если затраты в предмете полностью поглощается
                            {
                                _items[id].Quantity = _items[id].Item.Capacity;// Слот становится полным
                            }
                            else// Иначе
                            {
                                _items[id].Quantity += quantityDependency;// Слот пополняется не полностью
                                _items[i].Remove();// Дочерний предмет удаляется
                            }
                        }
                    }
                }
            }

            _controller.UpdateUI(_items);
        }

        [SerializeField] private List<int> _slotsSelected = new List<int>();
        [SerializeField] private Item _slotDragItem;
        [SerializeField] private int _slotDragQuantity;
        [SerializeField] private int _dragSelected;
        public void StartDrag(int id)
        {
            if (_items[id].IsEmpty())
                return;
            Item initItem = _items[id].Item;
            _slotDragQuantity = _items[id].Quantity;
            _slotDragItem = _items[id].Item;
            _slotsSelected.Add(id);
            UpdateSlotsDrag(_slotsSelected[0]);
        }
        public void AppendDrag(int id)
        {
            if (_slotsSelected.Count == 0)
                return;

            _slotDragItem = _items[_slotsSelected[0]].Item;
            _dragSelected = _slotsSelected[^1];

            for (int i = 0; i < _slotsSelected.Count; i++)
            {
                if (_slotsSelected[i] == id)// Если слот повторяется
                {
                    _dragSelected = id;
                    break;
                }
                if (_items[id].IsEmpty())// Если слот пустой
                {
                    if (_slotDragQuantity == _slotsSelected.Count)//Если больше нет количества для слотов
                        return;
                    _slotsSelected.Add(id);
                    break;
                }
            }

            UpdateSlotsDrag(_dragSelected);
        }
        private void UpdateSlotsDrag(int selected)
        {
            int whole = _slotDragQuantity / _slotsSelected.Count;
            int remainder = _slotDragQuantity - whole * _slotsSelected.Count;
            for (int i = 0; i < _slotsSelected.Count; i++)
                _items[_slotsSelected[i]].Add(_slotDragItem, whole);
            _items[selected].Quantity += remainder;

            _controller.SelectDrag(_slotsSelected);
            _controller.UpdateUI(_items);
        }
        public void FinishDrag()
        {
            _slotsSelected = new List<int>();
            _controller.DeselectAll();
            _controller.UpdateUI(_items);
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
            if (GUILayout.Button("Add Item Unstackable"))
            {
                holder.Add(ItemsHolder.Items[int.Parse(itemID)], int.Parse(quantity), false);
            }
            if (GUILayout.Button("Remove Item"))
            {
                holder.Remove(ItemsHolder.Items[int.Parse(itemID)], int.Parse(quantity));
            }
        }
    }
#endif
}