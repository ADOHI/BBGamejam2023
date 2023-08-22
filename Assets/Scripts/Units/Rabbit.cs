using BBGamejam.Global.Ingame;
using BBGamejam.Global.Mode;
using BBGamejam.Global.Particles;
using BBGamejam.Ingame.UI;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public class Rabbit : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private SphereCollider collider;
        //[SerializeField] private int health;
        [SerializeField] public int airMax;
        private int air;
        [SerializeField] private bool isImediately;
        private bool isCharging = false;
        [SerializeField] private bool isSeat = false;
        public SkinnedMeshRenderer targetRenderer;
        [HideInInspector] public Animator animator;

        private Coroutine AirChargeRoutine = null;
        private int seatCount = 0;
        private int dropCount = 0;
        [Header("AttackSetting")]
        public float attackRange = 1f;

        [Header("UIs")]
        private List<Image> airUIImages = new();
        public Transform uiTarget;
        public Vector3 airUIOffset;
        public GameObject airUIParent;
        public Image airUIImagePrefab;
        public Vector3 comboTextFromOffset;
        public Vector3 comboTextToOffset;
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            collider = GetComponent<SphereCollider>();
            UpdateAttackRange(attackRange);
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            airUIParent.transform.position = Camera.main.WorldToScreenPoint(uiTarget.position) + airUIOffset;
        }

        private void FixedUpdate()
        {
            if (isSeat)
            {
                Zara zara = (Managers.Scene.CurrentScene as InGameScene).Zara;
                gameObject.transform.position = zara.Seat.transform.position;
                gameObject.transform.rotation = zara.Seat.transform.rotation;
            }
        }

        private void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;

            air = airMax;
            SetMaxAirForUI(air);
        /*    for (int i = 0; i < air; i++)
            {
                ((UI_InGameScene)Managers.UI.SceneUI).AddRabbitAir();
            }*/

            Seat();

            //Zara zara = (Managers.Scene.CurrentScene as InGameScene).Zara;
            //gameObject.transform.position = zara.Seat.transform.position;
        }

        public void UpdateAttackRange(float newRange)
        {
            collider.radius = newRange;
        }

        public void UpgradeAttackRange()
        {
            collider.radius += 0.2f;
        }

        public void Push(Vector3 force)
        {
            animator.SetBool("isReady", false);
            animator.SetBool("isJump", true);
            if (air > 0)
            {
                _rigidbody.isKinematic = false;
                if (isSeat)
                {
                    Debug.Log("Push when seat");
                    isSeat = false;
                    IngameManager.Instance.ResetCombo();

                }

                transform.forward = force.normalized;
                Debug.Log(force.normalized);
                _rigidbody.AddForce(_rigidbody.mass * force, ForceMode.VelocityChange);
                UseAir();
            }
            else
            {
                if (air == 0)
                {
                    (Managers.Scene.CurrentScene as InGameScene).Zara.Damaged(++dropCount);
                    isCharging = false;
                    Seat();
                }
                Debug.Log("산소 추진체 부족");
            }
        }

        private void ChargeAir()
        {
            if (air < airMax)
            {
                Debug.Log("Charge Air");
                //(Managers.UI.SceneUI as UI_InGameScene).AddRabbitAir();
                air++;
                SetAirUI(air, airMax);
            }
        }

        private void UseAir()
        {
            if (air > 0)
            {
                Debug.Log("Use Air");
                //(Managers.UI.SceneUI as UI_InGameScene).DamageRabbitAir();
                air--;
                SetAirUI(air, airMax);
                Managers.Game.AirCount++;
            }
        }

        private void SetMaxAirForUI(int maxAir)
        {
            for (int i = 0; i < maxAir; i++)
            {
                var airUIImage = Instantiate(airUIImagePrefab);
                airUIImages.Add(airUIImage);
                airUIImage.transform.SetParent(airUIParent.transform);
                airUIImage.gameObject.SetActive(true);
            }
        }

        public void AddMaxAir(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var airUIImage = Instantiate(airUIImagePrefab);
                airUIImages.Add(airUIImage);
                airUIImage.transform.SetParent(airUIParent.transform);
                airUIImage.gameObject.SetActive(true);
            }

            this.airMax += amount;
        }

        public void SubMaxAir(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var airUIImage = Instantiate(airUIImagePrefab);
                airUIImages.Add(airUIImage);
                airUIImage.transform.SetParent(airUIParent.transform);
                airUIImage.gameObject.SetActive(true);

                var index = airUIImages.Count - 1;
                var ui = airUIImages[index];
                airUIImages.RemoveAt(index);
            }

            this.airMax -= amount;
        }

        private void SetAirUI(int air, int maxAir)
        {
            for (int i = 0; i < maxAir; i++)
            {
                if (i < air)
                {
                    airUIImages[i].enabled = true;
                }
                else 
                {
                    airUIImages[i].enabled = false;
                }
            }
        }

        private void Seat()
        {
            Debug.Log("Seat");
            animator.SetBool("isIdle", true);
            isSeat = true;
            SlowModeManager.Instance.PlayLandingSound();

            //hit
            var zaraPosition = (Managers.Scene.CurrentScene as InGameScene).Zara.transform.position;
            var rayDirection = (zaraPosition - transform.position).normalized;
            var distance = Vector3.Distance(zaraPosition, transform.position);
            var hits = Physics.SphereCastAll(uiTarget.transform.position, 1f, rayDirection, distance);

            if (hits != null)
            {
                foreach (var hitItem in hits)
                {
                    if (hitItem.collider.tag == "Enemy")
                    {
                        BubbleGenerator.Instance.Hit(hitItem.transform.position);
                        IngameManager.Instance.AddCombo();
                        IngameUIManager.Instance.SpawnComboTextAsync(hitItem.transform.position, Vector3.up, 3f, DG.Tweening.Ease.OutQuint);
                        hitItem.collider.GetComponent<Enemy>().Kill();
                    }
                }
            }

            //charge
            if (!isCharging)
            {
                if (isImediately)
                {
                    while (air < airMax)
                    {
                        ChargeAir();
                    }
                }
                else
                {
                    AirChargeRoutine = StartCoroutine(ChargeAirRoutine());
                }

                isCharging = true;
            }

            if (IngameManager.Instance.currentComboCount > 0)
            {
                IngameUIManager.Instance.SpawnFinalComboTextAsync(comboTextFromOffset, comboTextToOffset * 200f, 1f)
                    .AttachExternalCancellation(IngameUIManager.Instance.destroyCancellationToken)
                    .Forget();

                IngameManager.Instance.PlayComboSound();
                IngameManager.Instance.UpdateScore();
            }


            //Zara zara = (Managers.Scene.CurrentScene as InGameScene).Zara;
            //gameObject.transform.SetParent(zara.transform);
            //gameObject.transform.localPosition = zara.Seat.transform.localPosition;
            //gameObject.transform.localRotation = zara.Seat.transform.localRotation;

            //_rigidbody.velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                if (!isSeat && _rigidbody.velocity.magnitude > 0.1f)
                {
                    BubbleGenerator.Instance.Hit(other.transform.position);
                    AddTimeJitterAndKill(other).Forget();
                }
            }

            if (other.tag == "AirPocket")
            {
                Debug.Log("AirPocket Enter");
                Debug.Log(Time.frameCount);
                if (isSeat == false)
                {
                    if (seatCount == 0)
                    {
                        seatCount++;
                    }
                    else
                    {
                        Seat();
                    }
                }

            }
        }

        private async UniTask AddTimeJitterAndKill(Collider other)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            await UniTask.Delay(100);
            if (other != null)
            {
                IngameManager.Instance.AddCombo();
                IngameUIManager.Instance.SpawnComboTextAsync(other.transform.position, Vector3.up, 3f, DG.Tweening.Ease.OutQuint);
                other.GetComponent<Enemy>().Kill();
            }
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }

        private void OnTriggerStay(Collider other)
        {
            /*if (other.tag == "AirPocket" && isImediately && isCharging)
            {
                while (air < airMax)
                {
                    ChargeAir();
                }

            }*/
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "AirPocket")
            {
                Debug.Log(Time.frameCount);
                Debug.Log("Exit Air Pocket");
            }

            if (other.tag == "AirPocket" && isCharging)
            {
                isCharging = false;
                if (!AirChargeRoutine.IsUnityNull())
                {
                    StopCoroutine(AirChargeRoutine);
                    AirChargeRoutine = null;
                }
            }
        }

        private IEnumerator ChargeAirRoutine()
        {
            while (air < airMax)
            {
                yield return new WaitForSeconds(1);
                ChargeAir();
            }

            yield break;
        }
    }
}
