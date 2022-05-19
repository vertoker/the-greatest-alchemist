using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using Core.Items;
using Game.Pool;

namespace Core.UI
{
    public class InvertoryController : MonoBehaviour
    {
        [SerializeField] private ItemSlot[] _slots;
        private RectTransform _transform;
        [SerializeField] private int _selected = -1;

        private UnityAction<int, int> _switchItems;
        private UnityAction<int> _collectItem;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _slots = new ItemSlot[_transform.childCount];

            for (int i = 0; i < _transform.childCount; i++)
            {
                var slot = _transform.GetChild(i).GetComponent<ItemSlot>();
                _slots[i] = slot;
                _slots[i].SetID(i);
            }
        }
        public void Init(UnityAction<int, int> switchItems, UnityAction<int> collectItem, UnityAction<int> append, UnityAction<int> start, UnityAction finish)
        {
            _switchItems = switchItems;
            _collectItem = collectItem;
            InvertoryEventCaller.SetEvents(Click, append, start, finish);
        }

        public void UpdateUI(InvertoryItem[] items)
        {
            for (int i = 0; i < _transform.childCount; i++)
                _slots[i].UpdateItem(items[i]);
        }

        public void Click(int id)
        {
            //print(string.Join("  ", _selected, id));
            if (_selected == id)
            {
                _selected = -1;
                _slots[id].Deselect();
                _collectItem.Invoke(id);
                return;
            }
            if (_selected == -1)
            {
                _selected = id;
                _slots[id].Select();
                return;
            }
            _slots[_selected].Deselect();
            _switchItems.Invoke(_selected, id);
            _selected = -1;
        }
        public void SelectDrag(List<int> _slotsSelected)
        {
            foreach (var id in _slotsSelected)
                _slots[id].Select();
        }
        public void DeselectAll()
        {
            foreach (var slot in _slots)
                slot.Deselect();
            _selected = -1;
        }
    }
}