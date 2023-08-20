using TMPro;

namespace RabbitResurrection
{
    public class Content_Rank : UI_Base
    {
        enum Texts
        {
            Text_Rank,
            Text_Name,
            Text_Score,
        }

        private int rank;
        private string name;
        private int score;

        private TextMeshProUGUI text_Rank;
        private TextMeshProUGUI text_Name;
        private TextMeshProUGUI text_Score;

        public override void Init()
        {
            Bind<TextMeshProUGUI>(typeof(Texts));

            text_Rank = Get<TextMeshProUGUI>((int)Texts.Text_Rank);
            text_Name = Get<TextMeshProUGUI>((int)Texts.Text_Name);
            text_Score = Get<TextMeshProUGUI>((int)Texts.Text_Score);

            text_Rank.text = $"{rank}";
            text_Name.text = name;
            text_Score.text = $"{score}";
        }

        public void SetData(int rank, string name, int score)
        {
            this.rank = rank;
            this.name = name;
            this.score = score;
        }
    }
}