using UnityEngine.Events;
using UnityEngine;

namespace Core.Invertory
{
    public abstract class EventCaller : MonoBehaviour
    {
        public virtual void SetEvents(UnityAction<int> click, UnityAction<int> append, UnityAction<int> start, UnityAction finish)
        {

        }
    }
}
