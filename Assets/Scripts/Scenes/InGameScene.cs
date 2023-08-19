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

            StartPoint = new GameObject("@StartPoint");
            EndPoint = new GameObject("@EndPoint");

            StartPoint.transform.position = new Vector3(0, -10f, 0);
            EndPoint.transform.position = new Vector3(100, -50f, 0);
            CineCamera = Managers.Resource.Instantiate<CinemachineVirtualCamera>("Prefabs/@Virtual Camera");
            EnemyManager = Managers.Resource.Instantiate<EnemyManager>("Prefabs/@EnemyManager");
            CineTarget = Managers.Resource.Instantiate<CineTarget>("Prefabs/CineTarget");

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
