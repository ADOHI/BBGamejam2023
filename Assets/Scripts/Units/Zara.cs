using UnityEngine;
using UniRx;
using System.Collections;
using RabbitResurrection;
using TMPro;

public class Zara : MonoBehaviour
{
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int health;
    public GameObject Seat;
    public GameObject AirPocket;

    private Coroutine swimRoutine;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        transform.position = _startPoint.transform.position;

        for (int i = 0; i < health; i++)
        {
            ((UI_InGameScene)Managers.UI.SceneUI).AddZaraHealth();
        }

        if(_endPoint)
        {
            swimRoutine = StartCoroutine(StartSwim());
        }
    }

    public void SetData(GameObject startPoint, GameObject endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
    }

    public IEnumerator StartSwim()
    {
        while (Vector3.Distance(transform.position, _endPoint.transform.position) >= 1.0f)
        {
            Vector3 direction = _endPoint.transform.position - transform.position;
            transform.Translate(direction.normalized * _speed * Time.deltaTime * Managers.Game.RabbitTimeScale);
            //transform.position = Vector3.MoveTowards(transform.position, _endPoint.transform.position, _speed * Time.deltaTime * Managers.Game.RabbitTimeScale);
            transform.LookAt(_endPoint.transform.position);
            var position = transform.position;
            position.z = 0;
            transform.position = position;
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    public void Damaged()
    {
        if (health > 0)
        {
            (Managers.UI.SceneUI as UI_InGameScene).DamageZaraHealth();
            health--;

            if(health ==0)
            {
                Debug.Log("GameOver");
            }
        }
    }
}
