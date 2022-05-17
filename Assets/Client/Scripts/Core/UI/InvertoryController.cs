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
        private ItemSlot[] slots;
        private RectTransform _transform;
        [SerializeField] private int _selected = -1;
        private UnityAction<int, int> _switchItems;
        private UnityAction<int> _collectItem;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void Init(UnityAction<int, int> switchItems, UnityAction<int> collectItem)
        {
            _switchItems = switchItems;
            _collectItem = collectItem;
            slots = new ItemSlot[_transform.childCount];
            for (int i = 0; i < _transform.childCount; i++)
            {
                var slot = _transform.GetChild(i).GetComponent<ItemSlot>();
                slots[i] = slot;
                slots[i].Init(i, Click);
            }
        }

        public void UpdateUI(InvertoryItem[] items)
        {
            for (int i = 0; i < _transform.childCount; i++)
                slots[i].UpdateItem(items[i]);
        }

        public void Click(int id)
        {
            //print(string.Join("  ", _selected, id));
            if (_selected == id)
            {
                _selected = -1;
                slots[id].Deselect();
                _collectItem.Invoke(id);
                return;
            }
            if (_selected == -1)
            {
                _selected = id;
                slots[id].Select();
                return;
            }
            slots[_selected].Deselect();
            _switchItems.Invoke(_selected, id);
            _selected = -1;
        }
    }
}