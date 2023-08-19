using Cinemachine;
using UnityEngine;

namespace RabbitResurrection
{
    public class InGameScene : BaseScene
    {
        public GameObject StartPoint;
        public GameObject EndPoint;
        public Rabbit rabbit;
        public Zara Zara;
        public EnemyManager EnemyManager;
        public CinemachineVirtualCamera CineCamera;
        public CineTarget CineTarget;

        private void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();

            Managers.UI.ShowSceneUI<UI_InGameScene>();
            Zara = Managers.Resource.Instantiate<Zara>("Prefabs/Zara");
            Zara.SetData(StartPoint, EndPoint);

            rabbit = Managers.Resource.Instantiate<Rabbit>("Prefabs/Rabbit");

            for (int i = 0; i < 5; i++)
            {
                EnemyManager.SpawnEnemy(Zara.gameObject);
            }

            CineTarget.SetData(rabbit, Zara, CineCamera);
            CineCamera.Follow = CineTarget.gameObject.transform;
        }

        public override void Clear()
        {
            Debug.Log("Clear InGameScene");
        }
    }
}
