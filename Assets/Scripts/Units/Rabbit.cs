using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RabbitResurrection
{
    public class Rabbit : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        //[SerializeField] private int health;
        [SerializeField] private int airMax;
        private int air;
        [SerializeField] private bool isImediately;
        private bool isCharging = false;
        [SerializeField] private bool isSeat = false;

        private Coroutine AirChargeRoutine = null;

        private void Start()
        {
            Init();
        }

        private void FixedUpdate()
        {
            if (!isSeat)
            {
                transform.LookAt(Camera.main.transform);
                var position = transform.position;
                position.z = 0f;
                transform.position = position;
            }
        }

        private void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;

            air = airMax;

            for (int i = 0; i < air; i++)
            {
                ((UI_InGameScene)Managers.UI.SceneUI).AddRabbitAir();
            }

            Seat();
        }

        public void Push(Vector3 force)
        {
            if (air > 0)
            {
                _rigidbody.isKinematic = false;
                if (isSeat)
                {
                    gameObject.transform.SetParent(null);
                    gameObject.transform.rotation = Quaternion.identity;

                    isSeat = false;
                }
                _rigidbody.AddForce(_rigidbody.mass * force, ForceMode.Impulse);
                UseAir();

                if (air == 0)
                {
                    (Managers.Scene.CurrentScene as InGameScene).Zara.Damaged();
                    Seat();
                }
            }
            else
            {
                Debug.Log("산소 추진체 부족");
            }
        }

        private void ChargeAir()
        {
            if (air < airMax)
            {
                (Managers.UI.SceneUI as UI_InGameScene).AddRabbitAir();
                air++;
            }
        }

        private void UseAir()
        {
            if (air > 0)
            {
                (Managers.UI.SceneUI as UI_InGameScene).DamageRabbitAir();
                air--;
            }
        }

        private void Seat()
        {
            isSeat = true;

            Zara zara = (Managers.Scene.CurrentScene as InGameScene).Zara;
            gameObject.transform.SetParent(zara.transform);
            gameObject.transform.localPosition = zara.Seat.transform.localPosition;
            gameObject.transform.localRotation = zara.Seat.transform.localRotation;

            _rigidbody.velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Enemy")
            {
                if (_rigidbody.velocity.magnitude > 0.1f)
                {
                    other.GetComponent<Enemy>().Kill();
                }
            }

            if (other.tag == "AirPocket")
            {
                if (isSeat == false)
                {
                    Seat();
                }

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
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "AirPocket" && isImediately && isCharging)
            {
                while (air < airMax)
                {
                    ChargeAir();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
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
