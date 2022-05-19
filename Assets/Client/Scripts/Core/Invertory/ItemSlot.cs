using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Core.Items;

namespace Core.Invertory
{
    public class ItemSlot : MonoBehaviour
    {
        private TMP_Text _capacity;
        private Image _item, _background;

        private int _id;
        public int ID => _id;
        public void SetID(int id)
        {
            _id = id;
        }

        private void Awake()
        {
            _background = GetComponent<Image>();
            _item = transform.GetChild(1).GetComponent<Image>();
            _capacity = transform.GetChild(2).GetComponent<TMP_Text>();
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

        public void Select()
        {
            _background.color = Color.red;
        }
        public void Deselect()
        {
            _background.color = Color.white;
        }
    }
}