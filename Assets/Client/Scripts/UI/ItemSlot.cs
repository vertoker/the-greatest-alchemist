using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Core.Items;

namespace Core.UI
{
    public class ItemSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler
    {
        private TMP_Text _capacity;
        private Image _item;

        private void Awake()
        {
            _item = transform.GetChild(1).GetComponent<Image>();
            _capacity = transform.GetChild(2).GetComponent<TMP_Text>();
        }

        public void SetItem(Item item)
        {
            _item.sprite = item.Image;
            _capacity.text = item.ID.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }
        public void OnBeginDrag(PointerEventData eventData)
        {

        }
        public void OnDrag(PointerEventData eventData)
        {

        }
        public void OnPointerUp(PointerEventData eventData)
        {

        }
    }
}