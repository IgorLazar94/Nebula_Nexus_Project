using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class JoyStickInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Image jsContainer;
        private Image joystick;
        private Vector3 InputDirection;

        public static Action<Vector3> isHasInputDirection;
        public static Action isNotHasInputDirection;

        void Start()
        {
            jsContainer = GetComponent<Image>();
            joystick = transform.GetChild(0).GetComponent<Image>();
            InputDirection = Vector3.zero;
        }

        public void OnDrag(PointerEventData ped)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                    (jsContainer.rectTransform,
                    ped.position,
                    ped.pressEventCamera,
                    out position);

            position.x = (position.x / jsContainer.rectTransform.sizeDelta.x);
            position.y = (position.y / jsContainer.rectTransform.sizeDelta.y);

            InputDirection = new Vector3(position.x * 2 + 0, position.y * 2);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            isHasInputDirection?.Invoke(InputDirection);

            joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (jsContainer.rectTransform.sizeDelta.x / 3), InputDirection.y * (jsContainer.rectTransform.sizeDelta.y) / 3);
        }

        public void OnPointerDown(PointerEventData ped)
        {
            OnDrag(ped);
        }

        public void OnPointerUp(PointerEventData ped)
        {
            InputDirection = Vector3.zero;
            isNotHasInputDirection?.Invoke();
            joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}

