using UnityEngine;

namespace RabbitResurrection
{
    public class InGameScene : BaseScene
    {
        public GameObject StartPoint;
        public GameObject EndPoint;
        public Zara zara;

        private void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();

            zara = Managers.Resource.Instantiate<Zara>("Prefabs/Zara");
            zara.SetData(StartPoint, EndPoint);
            _ = StartCoroutine(zara.StartSwim());
        }

        public override void Clear()
        {
            Debug.Log("Clear InGameScene");
        }
    }
}
