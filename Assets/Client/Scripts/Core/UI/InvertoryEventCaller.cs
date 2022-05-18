using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
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
                    slot.StartDrag();
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
                        slot.AppendDrag();
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
                        slot.FinishDrag();
                    if (!_isDrag)
                        slot.Click();
                }
            }

            if (_isSelect)
            {
                _isSelect = false;
                _scrollRect.enabled = true;
            }
            _isDrag = false;
        }
    }
}