namespace RabbitResurrection
{
    public class EndScene : BaseScene
    {
        private void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();

            Managers.UI.ShowPopupUI<UI_GameClear>();
        }
        public override void Clear()
        {
        }
    }
}