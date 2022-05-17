using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Core.UI
{
    public class InvertoryEventCaller : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private float _timeToDisable = 0.3f;
        [SerializeField] private ScrollRect _scrollRect;
        private Coroutine _disablerScrollRect;

        [SerializeField] private InvertoryHolder _holder;
        private List<RaycastResult> _results;
        private GraphicRaycaster _raycaster;

        private void Awake()
        {
            _raycaster = GetComponent<GraphicRaycaster>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_scrollRect.enabled)
            {
                _results = new List<RaycastResult>();
                _raycaster.Raycast(eventData, _results);
                if (_results[0].gameObject.TryGetComponent(out ItemSlot slot))
                    slot.Click();
            }
            if (_disablerScrollRect != null)
            {
                StopCoroutine(_disablerScrollRect);
                _scrollRect.enabled = true;
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _disablerScrollRect = StartCoroutine(DisablerScrollRect());
        }
        public void OnDrag(PointerEventData eventData)
        {
            _results = new List<RaycastResult>();
            _raycaster.Raycast(eventData, _results);
            if (_results[0].gameObject.TryGetComponent(out ItemSlot slot))
                print(_results[0].gameObject.name);
        }

        private IEnumerator DisablerScrollRect()
        {
            yield return new WaitForSeconds(_timeToDisable);
            _scrollRect.enabled = false;
        }
    }
}