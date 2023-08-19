using Cinemachine;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
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

        public GameObjectReference rabbitReference;
        public GameObjectReference zaraReference;

        [Header("Setting")]
        public Vector3 startPosition;
        public Vector3 endPosition;
        public bool isSpawnAutomatically;
        public Vector3 cineTargetOffset;
        private void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();

            if (isSpawnAutomatically)
            {
                StartPoint = new GameObject("@StartPoint");
                EndPoint = new GameObject("@EndPoint");
                StartPoint.transform.position = startPosition;
                EndPoint.transform.position = endPosition;
                CineCamera = Managers.Resource.Instantiate<CinemachineVirtualCamera>("Prefabs/@Virtual Camera");
                Camera.main.GetOrAddComponent<CinemachineBrain>();
                EnemyManager = Managers.Resource.Instantiate<EnemyManager>("Prefabs/@EnemyManager");
                
                CineTarget = Managers.Resource.Instantiate<CineTarget>("Prefabs/CineTarget");
                //Managers.UI.ShowSceneUI<UI_InGameScene>();
                Zara = Managers.Resource.Instantiate<Zara>("Prefabs/Zara");
                //Zara.SetData(StartPoint, EndPoint);
                rabbit = Managers.Resource.Instantiate<Rabbit>("Prefabs/Rabbit");

            }
            else
            {

            }
            Managers.UI.ShowSceneUI<UI_InGameScene>();
            Zara.SetData(StartPoint, EndPoint);
            EnemyManager.SetData(Zara.gameObject);
            CineTarget.SetData(rabbit, Zara, CineCamera, cineTargetOffset);
            CineCamera.Follow = CineTarget.gameObject.transform;
            rabbitReference.Value = rabbit.gameObject;
            zaraReference.Value = Zara.gameObject;

            Managers.Game.GameStart();
        }

        public override void Clear()
        {
            Debug.Log("Clear InGameScene");
        }
    }
}
