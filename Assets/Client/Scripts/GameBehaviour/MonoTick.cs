using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameBehaviour
{
    public abstract class MonoTick : MonoBehaviour
    {
        [SerializeField] private TickType _tickType;
        public virtual void Tick()
        {

        }

        protected virtual void OnEnable()
        {
            TickUpdate.AddFixedUpdate(_tickType, Tick);
        }
        protected virtual void OnDisable()
        {
            TickUpdate.RemoveFixedUpdate(_tickType, Tick);
        }
    }
}