using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RabbitResurrection
{
    public class InputManager
    {
        public EventSystem EventSystem { get; private set; }
        public Action KeyAction = null;
        public Action<Define.MouseEvent> MouseAction = null;

        bool _pressed = false;

        public void Init()
        {

        }

        public void OnUpdate()
        {
            if (Input.anyKey && KeyAction != null)
            {
                KeyAction.Invoke();
            }

            if (MouseAction != null)
            {
                if (Input.GetMouseButton(0))
                {
                    MouseAction.Invoke(Define.MouseEvent.Press);
                    _pressed = true;
                }
                else
                {
                    if (_pressed)
                    {
                        MouseAction.Invoke(Define.MouseEvent.Click);
                        _pressed = false;
                    }
                }
            }
        }

        public void Clear()
        {
            KeyAction = null;
            MouseAction = null;
        }
    }
}