using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public class UI_Opening : UI_Popup
    {
        enum Buttons
        {
            Button_Next
        }

        public List<GameObject> contents = new List<GameObject>();

        private Button button_Next;

        private int index = 0;
        public override void Init()
        {
            base.Init();

            Bind<Button>(typeof(Buttons));

            button_Next = Get<Button>((int)Buttons.Button_Next);
            button_Next.onClick.AddListener(() => OnNext());
        }

        private void OnNext()
        {
            contents[index].SetActive(false);
            index++;
            if (index < 3)
            {
                contents[index].SetActive(true);
            }
            else
            {
                //Managers.Scene.LoadScene(Define.Scene.TitleScene);
                //Managers.Scene.LoadScene(Define.Scene.TitleScene);
                Managers.Clear();
                SceneManager.LoadScene(1);
            }
        }
    }
}