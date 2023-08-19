using UnityEngine;

namespace RabbitResurrection
{
    public class GameManager
    {
        public bool IsGameOver = false;
        public int KillCount;
        public int AirCount;

        public void GameStart()
        {
            IsGameOver = false;
            Time.timeScale = 1.0f;
        }

        public void GameOver()
        {
            IsGameOver = true;
            Time.timeScale = 0.0f;

            Managers.UI.ShowPopupUI<UI_GameOver>();
        }
    }
}