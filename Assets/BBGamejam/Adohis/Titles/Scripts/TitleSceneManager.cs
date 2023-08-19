using BBGamejam.Global;
using Cysharp.Threading.Tasks;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BBGamejam.Title
{
    public class TitleSceneManager : Singleton<TitleSceneManager>
    {

        public GameObject titleObject;
        public GameObject pressSpaceKeyText;

        private async UniTask StartTitleSceneAsync()
        {
            await GlobalUIManager.Instance.FadeInAsync(3f, GlobalUIManager.FadeImageType.White);
            ShowTitle();

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        private void ShowTitle()
        {

        }

        
    }

}
