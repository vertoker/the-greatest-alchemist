using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    public class ItemsHolder : MonoBehaviour
    {
        public static ItemsHolder Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}