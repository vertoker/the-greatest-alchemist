using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    [CreateAssetMenu(fileName = "Data/Item", menuName = "NewItem")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _maxCapacity;
        [SerializeField] private short _id;
        [SerializeField] private Sprite _image;

        public string Name => _name;
        public int MaxCapacity => _maxCapacity;
        public short ID => _id;
        public Sprite Image => _image;
    }
}