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
        [SerializeField] private TMP_Text _capacity;
        [SerializeField] private Image _item, _background;

        public int ID;

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