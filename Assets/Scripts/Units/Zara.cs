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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        transform.position = _startPoint.transform.position;
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
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.transform.position, _speed * Time.deltaTime);
            transform.LookAt(_endPoint.transform.position);

            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
}
