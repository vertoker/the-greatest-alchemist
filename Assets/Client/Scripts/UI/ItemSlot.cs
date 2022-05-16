using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class ItemSlot : MonoBehaviour
    {
        private TMP_Text _capacity;
        private Image _item;

        private void Awake()
        {
            _item = transform.GetChild(1).GetComponent<Image>();
            _capacity = transform.GetChild(2).GetComponent<TMP_Text>();
        }
    }
}