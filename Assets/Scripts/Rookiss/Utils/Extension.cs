﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RabbitResurrection
{
    public static class Extension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            return Util.GetOrAddComponent<T>(go);
        }

        public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
        {
            UI_Base.BindEvent(go, action, type);
        }
    }
}