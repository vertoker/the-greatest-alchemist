using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.Items;

namespace Core.Craft.Recipe
{
    [CreateAssetMenu(menuName = "Recipes/Simple", fileName = "NewSimpleRecipe", order = 1)]
    public class SimpleRecipe : ScriptableObject
    {
        [SerializeField] private InvertoryItem _item1, _item2, _item3, _item4;
        [SerializeField] private InvertoryItem _resultItem;
    }
}