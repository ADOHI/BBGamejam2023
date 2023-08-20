using System.Collections.Generic;
using UnityEngine;

namespace RabbitResurrection
{
    public class GameManager
    {
        public Dictionary<string, int> Ranking = new Dictionary<string, int>();

        public bool IsGameOver = false;
        public int KillCount;
        public int AirCount;

        public void GameStart()
        {
            IsGameOver = false;
            Time.timeScale = 1.0f;

            KillCount = 0;
            AirCount = 0;
        }

        public void GameOver()
        {
            IsGameOver = true;
            Time.timeScale = 0.0f;

            Managers.UI.ShowPopupUI<UI_GameOver>();
        }

        public void GameClear()
        {
            Managers.Scene.LoadScene(Define.Scene.EndScene);
        }

        public void Enroll(string name, int score)
        {
            Ranking.Add(name, score);
        }
    }
}