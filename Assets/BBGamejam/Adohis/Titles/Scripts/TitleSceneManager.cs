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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadSceneAsync("EnvironmentScene");
            }
        }

        private async UniTask StartTitleSceneAsync()
        {
            await GlobalUIManager.Instance.FadeInAsync(3f, GlobalUIManager.FadeImageType.White);
        }

        private void ShowTitle()
        {

        }

        
    }

}
