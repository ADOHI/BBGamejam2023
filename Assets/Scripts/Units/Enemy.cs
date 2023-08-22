using BBGamejam.Global.Ingame;
using System.Collections;
using UnityEngine;

namespace RabbitResurrection
{
    public class Enemy : MonoBehaviour
    {
        public enum SwimType
        {
            Chase,
            Wave,
            Vertical,
            Bomb
        }
        [SerializeField] private GameObject _target;
        [SerializeField] private float _speed = 7f;
        private Coroutine swimRoutine;
        private Coroutine bombRoutine;
        private Rigidbody rb;
        public SwimType swimType;
        public float explosionDistance = 10f;
        public float explosionRadius = 5f;
        public float explosionDuration = 1f;
        public float explosionPower = 1000f;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>(); 
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if(_target)
            {
                swimRoutine = StartCoroutine(StartSwimByType());
            }
        }

        private void Update()
        {
            var direction = _target.transform.position - transform.position;
            var xSign = Mathf.Sign(direction.x);
            transform.forward = Vector3.right * xSign;
        }

        public void SetData(GameObject target)
        {
            _target = target;
        }

        public IEnumerator StartSwim()
        {
            while (Vector3.Distance(transform.position, _target.transform.position) >= 2.0f)
            {
                Vector3 direction = _target.transform.position - transform.position;
                transform.Translate(direction.normalized * _speed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                //transform.LookAt(_target.transform.position);

                yield return null;
            }

            _target.GetComponent<Zara>().Damaged();
            StopCoroutine(swimRoutine);
            Managers.Resource.Destroy(gameObject);
            yield break;
        }

        public IEnumerator StartSwimByType()
        {
            switch (swimType)
            {
                case SwimType.Chase:
                    while (Vector3.Distance(transform.position, _target.transform.position) <= 100f)
                    {
                        Vector3 direction = _target.transform.position - transform.position;
                        rb.AddForce(direction.normalized * _speed * Time.fixedDeltaTime);
                        // transform.Translate(direction.normalized * _speed * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                        //transform.LookAt(_target.transform.position);

                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case SwimType.Wave:
                    while (Vector3.Distance(transform.position, _target.transform.position) <= 100f)
                    {
                        Vector3 direction = (_target.transform.position - transform.position).normalized;
                        
                        var upwardDirection = (direction.normalized + Vector3.up * 0.4f);
                        //transform.Translate(direction.normalized * _speed * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                        //transform.LookAt(_target.transform.position);
                        rb.AddForce(upwardDirection * _speed * Time.fixedDeltaTime, ForceMode.Impulse);
                        yield return new WaitForSeconds(2f);
                    }
                    break;
                case SwimType.Vertical:
                    while (Vector3.Distance(transform.position, _target.transform.position) <= 100f)
                    {
                        Vector3 direction = _target.transform.position - transform.position;
                        var downDirection = Mathf.Sign(direction.x) * Vector3.right + Vector3.down;
                        rb.AddForce(downDirection.normalized * _speed * Time.fixedDeltaTime);
                        // transform.Translate(direction.normalized * _speed * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                        //transform.LookAt(_target.transform.position);

                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case SwimType.Bomb:
                    while (Vector3.Distance(transform.position, _target.transform.position) <= 100f)
                    {
                        Vector3 direction = _target.transform.position - transform.position;
                        rb.AddForce(direction.normalized * _speed * Time.fixedDeltaTime);
                        // transform.Translate(direction.normalized * _speed * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                        //transform.LookAt(_target.transform.position);

                        yield return new WaitForFixedUpdate();

                        if (Vector3.Distance(transform.position, IngameManager.Instance.rabbit.Value.transform.position) < explosionDistance)
                        {
                            yield return StartCoroutine(Explosion(explosionPower));
                            break;
                        }
                    }
                    break;
            }
            Managers.Resource.Destroy(gameObject);
            yield break;
        }

        private IEnumerator Explosion(float power)
        {
            yield return new WaitForSeconds(explosionDuration);
            var colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var item in colliders)
            {
                if (item.tag == "Player" || item.tag == "Enemy")
                {
                    var direction = (item.transform.position - transform.position).normalized;
                    var targetRb = item.GetComponent<Rigidbody>();
                    targetRb.velocity = Vector3.zero;
                    item.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
                }
            }
            //Managers.Resource.Destroy(gameObject);
        }

        public void Kill()
        {
            StopCoroutine(swimRoutine);
            //(Managers.UI.SceneUI as UI_InGameScene).AddKillCount();
            Managers.Game.KillCount++;
            Managers.Resource.Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ZaraHitArea")
            {
                _target.GetComponent<Zara>().Damaged();
                StopCoroutine(swimRoutine);
                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}