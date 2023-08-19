using BBGamejam.Global;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BBGamejam.Title
{
    public class TitleSceneManager : Singleton<TitleSceneManager>
    {
        private Rigidbody targetRb;
        public GameObject titleObject;
        public GameObject pressSpaceKeyText;
        public GameObject turtle;
        [Header("CameraSetting")]
        private bool isLockon;
        public float lerpValue = 5f;
        public GameObject target;
        public float dropForce;
        public float stopHeight;
        public float releaseHeight;
        public Camera firstCamera;
        public Camera secondCamera;
        public Vector3 offset;
        [Header("TimeSetting")]
        public float showTitleInterval = 1f;
        public float pressKeyInterval = 1f;
        public int twinkleCount = 3;
        public float twinkleInterval = 0.3f;
        public float showRabbitUI = 3f;
        private void Awake()
        {
            GlobalUIManager.Instance.whiteFadeImage.color = Color.white;
            titleObject.SetActive(false);
            pressSpaceKeyText.SetActive(false);
        }

        private void Start()
        {
            StartTitleSceneAsync().AttachExternalCancellation(this.destroyCancellationToken).Forget();
        }

        private void LateUpdate()
        {
            if (isLockon)
            {
                var toDirection = (target.transform.position + offset) - firstCamera.transform.position;
                //firstCamera.transform.forward = Vector3.Lerp(firstCamera.transform.forward, toDirection, Time.deltaTime * lerpValue);
                firstCamera.transform.forward = toDirection;
            }
        }

        private async UniTask StartTitleSceneAsync()
        {
            await GlobalUIManager.Instance.FadeInAsync(3f, GlobalUIManager.FadeImageType.White);
            
            await UniTask.Delay((int)(showTitleInterval * 1000f));
            
            ShowTitle();
            
            await UniTask.Delay((int)(pressKeyInterval * 1000f));
            
            ShowPressSpaceText();
            
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            await TwinklePressSpaceText();

            titleObject.SetActive(false);

            await UniTask.Delay(1000);

            await firstCamera.transform.DORotate(new Vector3(-20f, -24f, 0f), 5f);

            DropRabbit();

            await UniTask.WaitUntil(() => target.transform.position.y < stopHeight);

            secondCamera.enabled = true;
            //Camera.ma = secondCamera;
            firstCamera.enabled = false;

            Time.timeScale = 0.0f;

            await UniTask.Delay((int)(showRabbitUI * 1000f), true);

            turtle.SetActive(false);

            firstCamera.enabled = true;
            //Camera.ma = secondCamera;
            secondCamera.enabled = false;

            Time.timeScale = 1.0f;

            await UniTask.Delay(500);

            await SceneManager.LoadSceneAsync(1);
        }

        private void ShowTitle()
        {
            titleObject.SetActive(true);
        }

        private void ShowPressSpaceText()
        {
            pressSpaceKeyText.SetActive(true);
        }

        private async UniTask TwinklePressSpaceText()
        {
            for (int i = 0; i < twinkleCount; i++)
            {
                pressSpaceKeyText.SetActive(false);
                await UniTask.Delay((int)(twinkleInterval * 1000f));
                pressSpaceKeyText.SetActive(true);
                await UniTask.Delay((int)(twinkleInterval * 1000f));
            }
            pressSpaceKeyText.SetActive(false);
        }

        private void DropRabbit()
        {
            isLockon = true;
            targetRb = target.GetComponent<Rigidbody>();
            targetRb.useGravity = true;
            targetRb.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
        }
    }

}
