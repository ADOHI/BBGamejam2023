using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public class UI_Ending : UI_Popup
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
            if (index < 6)
            {
                contents[index].SetActive(true);
            }
            else
            {
                Managers.UI.ShowPopupUI<UI_GameClear>();
            }
        }
    }
}