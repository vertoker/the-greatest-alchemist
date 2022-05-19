using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace Core.UI
{
    public class InvertoryEventCaller : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        private Vector2 _startPosition;
        private bool _isSelect = false;
        private bool _isDrag = false;

        [SerializeField] private InvertoryHolder _holder;
        [SerializeField] private GraphicRaycaster _raycaster;
        private List<RaycastResult> _results;

        private static UnityAction<int> _click;
        private static UnityAction<int> _append;
        private static UnityAction<int> _start;
        private static UnityAction _finish;

        public static void SetEvents(UnityAction<int> click, UnityAction<int> append, UnityAction<int> start, UnityAction finish)
        {
            _click = click;
            _append = append;
            _finish = finish;
            _start = start;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _startPosition = eventData.pointerCurrentRaycast.screenPosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDrag = true;
            Vector2 offset = eventData.pointerCurrentRaycast.screenPosition - _startPosition;
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x))
                return;

            _isSelect = true;
            _results = new List<RaycastResult>();
            _raycaster.Raycast(eventData, _results);
            if (_results.Count != 0)
                if (_results[0].gameObject.TryGetComponent(out ItemSlot slot))
                    _start.Invoke(slot.ID);
            _scrollRect.enabled = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!_scrollRect.enabled) 
            {
                _results = new List<RaycastResult>();
                _raycaster.Raycast(eventData, _results);
                if (_results.Count != 0)
                    if (_results[0].gameObject.TryGetComponent(out ItemSlot slot))
                        _append.Invoke(slot.ID);
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _results = new List<RaycastResult>();
            _raycaster.Raycast(eventData, _results);
            if (_results.Count != 0)
            {
                if (_results[0].gameObject.TryGetComponent(out ItemSlot slot))
                {
                    if (_isSelect)
                        _finish.Invoke();
                    if (!_isDrag)
                        _click.Invoke(slot.ID);
                }
            }

            if (_isSelect)
            {
                _isSelect = false;
                _scrollRect.enabled = true;
            }
            _isDrag = false;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                _finish.Invoke();
            }
        }
        private void OnApplicationQuit()
        {
            _finish.Invoke();
        }
    }
}