namespace RabbitResurrection
{
    public class OpenScene : BaseScene
    {
        private void Start()
        {
            Init();
        }
        protected override void Init()
        {
            base.Init();

            Managers.UI.ShowPopupUI<UI_Opening>();
            Managers.Sound.Play("Sounds/Opening", Define.Sound.BGM);
        }

        public override void Clear()
        {
            Managers.Sound.Clear();
        }
    }
}