using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RabbitResurrection
{
    public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnBeginDragHandler = null;
        public Action<PointerEventData> OnDragHandler = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null) { OnClickHandler.Invoke(eventData); }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (OnBeginDragHandler != null) { OnBeginDragHandler.Invoke(eventData); }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null) { OnDragHandler.Invoke(eventData); }
        }
    }
}