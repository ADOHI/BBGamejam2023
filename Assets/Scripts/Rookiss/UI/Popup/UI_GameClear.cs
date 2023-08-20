using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace RabbitResurrection
{
    public class UI_GameClear : UI_Popup
    {
        enum Texts
        {
            Text_Kill,
            Text_Jump,
            Text_Score,
            Shadow_Kill,
            Shadow_Jump,
            Shadow_Score
        }

        enum Objects
        {
            InputField,
            Panel_Rank,
        }

        enum Buttons
        {
            Button_Submit,
        }

        private TextMeshProUGUI text_Kill;
        private TextMeshProUGUI shadow_Kill;
        private TextMeshProUGUI text_Jump;
        private TextMeshProUGUI shadow_Jump;
        private TextMeshProUGUI text_Score;
        private TextMeshProUGUI shadow_Score;

        private InputField inputField;
        private GameObject panel_Rank;

        private Button button_Submit;

        public override void Init()
        {
            base.Init();

            Bind<TextMeshProUGUI>(typeof(Texts));
            Bind<GameObject>(typeof(Objects));
            Bind<Button>(typeof(Buttons));

            text_Kill = Get<TextMeshProUGUI>((int)Texts.Text_Kill);
            shadow_Kill = Get<TextMeshProUGUI>((int)Texts.Shadow_Kill);
            text_Kill.text = $"KILL {Managers.Game.KillCount}";
            shadow_Kill.text = $"KILL {Managers.Game.KillCount}";

            text_Jump = Get<TextMeshProUGUI>((int)Texts.Text_Jump);
            shadow_Jump = Get<TextMeshProUGUI>((int)Texts.Shadow_Jump);
            text_Jump.text = $"JUMP {Managers.Game.AirCount}";
            shadow_Jump.text = $"JUMP {Managers.Game.AirCount}";

            text_Score = Get<TextMeshProUGUI>((int)Texts.Text_Score);
            shadow_Score = Get<TextMeshProUGUI>((int)Texts.Shadow_Score);

            int score = (Managers.Game.KillCount / Managers.Game.AirCount) * 1000;
            text_Score.text = $"SCORE {score}";
            shadow_Score.text = $"SCORE {score}";

            inputField = Get<InputField>((int)Objects.InputField);
            panel_Rank = Get<GameObject>((int)Objects.Panel_Rank);
            button_Submit = Get<Button>((int)Buttons.Button_Submit);

            inputField.onEndEdit.AddListener((text) =>
            {
                Managers.Game.Enroll(text, score);
                UpdateBoard();
                button_Submit.interactable = false;
            });

            button_Submit.onClick.AddListener(() =>
            {
                Managers.Game.Enroll(inputField.text, score);
                UpdateBoard();
                button_Submit.interactable = false;
            });
        }

        private void UpdateBoard()
        {
            foreach(Transform child in panel_Rank.transform)
            {
                Managers.Resource.Destroy(child.gameObject);
            }

            var sortedTop5 = Managers.Game.Ranking
                .OrderByDescending(pair => pair.Value)
                .Take(5)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            int rank = 1;

            foreach(var item in sortedTop5)
            {
                Managers.UI.MakeSubItem<Content_Rank>(panel_Rank.transform).SetData(rank, item.Key, item.Value);
                rank++;
            }
        }
    }
}