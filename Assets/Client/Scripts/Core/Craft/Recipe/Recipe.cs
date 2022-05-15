using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.Items;

namespace Core.Craft.Recipe
{
    public abstract class Recipe : ScriptableObject
    {
        public virtual bool CanCraft(InvertoryItem[] items)
        {
            return false;
        }
    }
}