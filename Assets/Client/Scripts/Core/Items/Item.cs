using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Items
{
    [CreateAssetMenu(menuName = "Data/Item", fileName = "NewItem")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _maxCapacity = 1;
        [SerializeField] private int _id;
        [SerializeField] private Sprite _image;

        public string Name => _name;
        public int MaxCapacity => _maxCapacity;
        public int ID => _id;
        public Sprite Image => _image;

        
#if UNITY_EDITOR
        public void RandomizeID()
        {
            _id = Random.Range(int.MinValue, int.MaxValue);
        }
#endif
    }

#if UNITY_EDITOR
[CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Item item = (Item)target;
            GUILayout.Space(25);

            if (GUILayout.Button("RandomizeID"))
            {
                item.RandomizeID();
            }
        }
    }
#endif
}