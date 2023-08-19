using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public abstract class BaseScene : MonoBehaviour
    {
        public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            Camera camera = FindObjectOfType<Camera>();
            if(camera == null)
            {
                //Managers.Resource.Instantiate(PrefabPath.MainCamera).name = "@MainCamera";
            }
            else
            {
                camera.gameObject.name = "@MainCamera";
                camera.gameObject.GetOrAddComponent<Physics2DRaycaster>();
            }

            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                eventSystem = Managers.Resource.Instantiate("Prefabs/EventSystem").GetComponent<EventSystem>();
            }

            eventSystem.gameObject.name = "@EventSystem";
        }

        public abstract void Clear();
    }
}