using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;
using System;

namespace GameBehaviour
{
    public class TickUpdate : MonoInit
    {
        private static Dictionary<TickType, UnityEvent> _updateEvents = new Dictionary<TickType, UnityEvent>();
        private static Dictionary<TickType, UnityEvent> _fixedUpdateEvents = new Dictionary<TickType, UnityEvent>();

        private Coroutine _fixedUpdater, _updater;

        public static void AddUpdate(TickType key, UnityAction action)
        {
            _updateEvents[key].AddListener(action);
        }
        public static void AddFixedUpdate(TickType key, UnityAction action)
        {
            _fixedUpdateEvents[key].AddListener(action);
        }
        public static void RemoveUpdate(TickType key, UnityAction action)
        {
            _updateEvents[key].RemoveListener(action);
        }
        public static void RemoveFixedUpdate(TickType key, UnityAction action)
        {
            _fixedUpdateEvents[key].RemoveListener(action);
        }

        public override void Init()
        {
            Array types = Enum.GetValues(typeof(TickType));
            foreach (byte type in types)
            {
                _updateEvents.Add((TickType)type, new UnityEvent());
                _fixedUpdateEvents.Add((TickType)type, new UnityEvent());
            }
        }
        private void OnEnable()
        {
            WaitForSeconds waitFixed = new WaitForSeconds(Time.fixedTime);
            _updater = StartCoroutine(UpdateCoroutine());
            _fixedUpdater = StartCoroutine(FixedUpdateCoroutine(waitFixed));
        }
        private void OnDisable()
        {
            StopCoroutine(_updater);
            StopCoroutine(_fixedUpdater);
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                foreach (var tickEvent in _updateEvents)
                    tickEvent.Value.Invoke();
                yield return null;
            }
        }
        private IEnumerator FixedUpdateCoroutine(WaitForSeconds waitFixed)
        {
            while (true)
            {
                foreach (var tickEvent in _fixedUpdateEvents)
                    tickEvent.Value.Invoke();
                yield return waitFixed;
            }
        }
    }
}
