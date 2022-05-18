using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Core.Items;

namespace Core.UI
{
    public class ItemSlot : MonoBehaviour
    {
        private TMP_Text _capacity;
        private Image _item, _background;
        private static UnityAction<int> _click;
        private static UnityAction<int> _append;
        private static UnityAction<int> _start;
        private static UnityAction _finish;
        private int _id;

        private void Awake()
        {
            _background = GetComponent<Image>();
            _item = transform.GetChild(1).GetComponent<Image>();
            _capacity = transform.GetChild(2).GetComponent<TMP_Text>();
        }

        public static void Init(UnityAction<int> click, UnityAction<int> append, UnityAction<int> start, UnityAction finish)
        {
            _click = click;
            _append = append;
            _finish = finish;
            _start = start;
        }
        public void Init(int id)
        {
            _id = id;
        }
        public void UpdateItem(InvertoryItem item)
        {
            if (item == null)
            {
                _item.sprite = null;
                _capacity.text = string.Empty;
            }
            else if (item.IsEmpty())
            {
                _item.sprite = null;
                _capacity.text = string.Empty;
            }
            else
            {
                _item.sprite = item.Item.Image;
                _capacity.text = item.Quantity.ToString();
            }
        }

        public void Click()
        {
            _click.Invoke(_id);
        }
        public void Select()
        {
            _background.color = Color.red;
        }
        public void Deselect()
        {
            _background.color = Color.white;
        }

        public void StartDrag()
        {
            print("StartDrag");
            _start.Invoke(_id);
        }
        public void AppendDrag()
        {
            print("AppendDrag");
            _append.Invoke(_id);
        }
        public void FinishDrag()
        {
            print("FinishDrag");
            _finish.Invoke();
        }
    }
}