using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WindowManager : MonoBehaviour
    {
        [SerializeField] private int _activeWindowIndex = 0;
        [SerializeField] private GameObject[] _windows;

        private void Awake()
        {
            _windows = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _windows[i] = transform.GetChild(i).gameObject;
                _windows[i].SetActive(false);
            }
            _windows[_activeWindowIndex].SetActive(true);
        }

        public void Switch(int activeWindowIndex)
        {
            _windows[_activeWindowIndex].SetActive(false);
            _activeWindowIndex = activeWindowIndex;
            _windows[_activeWindowIndex].SetActive(true);
        }
    }
}