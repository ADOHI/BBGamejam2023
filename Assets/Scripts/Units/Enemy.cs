using System.Collections;
using UnityEngine;

namespace RabbitResurrection
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private float _speed = 7f;
        private Coroutine swimRoutine;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if(_target)
            {
                swimRoutine = StartCoroutine(StartSwim());
            }
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

        public void Kill()
        {
            StopCoroutine(swimRoutine);
            //(Managers.UI.SceneUI as UI_InGameScene).AddKillCount();
            Managers.Game.KillCount++;
            Managers.Resource.Destroy(gameObject);
        }
    }
}