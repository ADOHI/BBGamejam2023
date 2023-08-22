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

            //Managers.UI.ShowPopupUI<UI_Ending>();
            Managers.Sound.Play("Sounds/Ending", Define.Sound.BGM);
        }
        public override void Clear()
        {
            Managers.Sound.Clear();
        }
    }
}