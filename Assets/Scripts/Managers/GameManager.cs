namespace RabbitResurrection
{
    public class GameManager
    {
        public float RabbitTimeScale = 1.0f;

        public void GameOver()
        {
            RabbitTimeScale = 0.0f;
        }
    }
}