using UnityEngine;

namespace RabbitResurrection
{
    public class GameManager
    {
        public bool IsGameOver = false;

        public void GameOver()
        {
            IsGameOver = true;
            Time.timeScale = 0.0f;

            Managers.UI.ShowPopupUI<UI_GameOver>();
        }
    }
}