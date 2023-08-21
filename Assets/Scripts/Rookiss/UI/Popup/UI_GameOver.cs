using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public class UI_GameOver : UI_Popup
    {
        private enum Buttons
        {
            Button_Retry,
            Button_ToTitle,
        }

        private Button button_Retry;
        private Button button_ToTitle;

        public override void Init()
        {
            base.Init();

            Bind<Button>(typeof(Buttons));

            button_Retry = Get<Button>((int)Buttons.Button_Retry);
            button_ToTitle = Get<Button>((int)Buttons.Button_ToTitle);

            button_Retry.onClick.AddListener(() => Managers.Scene.LoadScene(Define.Scene.MergeScene_Environment));
            button_ToTitle.onClick.AddListener(() =>
            {
                Managers.Clear();
                SceneManager.LoadScene(1);
                //Managers.Scene.LoadScene(Define.Scene.TitleScene);
                });
        }
    }
}