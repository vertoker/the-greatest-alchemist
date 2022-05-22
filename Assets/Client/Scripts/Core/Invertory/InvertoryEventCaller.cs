using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Core.Invertory
{
    public class InvertoryEventCaller : EventCaller, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        private Vector2 _startPosition;
        private bool _isSelect = false;
        private bool _isDrag = false;
        private bool _isDown = false;

        [SerializeField] private InvertoryHolder _holder;
        [SerializeField] private GraphicRaycaster _raycaster;
        private List<RaycastResult> _results;

        private UnityAction<int> _click;
        private UnityAction<int> _append;
        private UnityAction<int> _start;
        private UnityAction _finish;

        public override void SetEvents(UnityAction<int> click, UnityAction<int> append, UnityAction<int> start, UnityAction finish)
        {
            _click = click;
            _append = append;
            _start = start;
            _finish = finish;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _isDown = true;
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
            {
                foreach (var result in _results)
                {
                    if (result.gameObject.TryGetComponent(out ItemSlot slot))
                    {
                        _start.Invoke(slot.ID);
                        break;
                    }
                }
            }

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
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isDown)
                return;
            Debug.Log("OnPointerExit");

            if (_isSelect)
            {
                if (_isDrag)
                    _finish.Invoke();
                _isSelect = false;
                _scrollRect.enabled = true;
            }
            _isDrag = false;
            _isDown = false;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDown)
                return;
            Debug.Log("OnPointerUp");

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
            _isDown = false;
        }
    }
}