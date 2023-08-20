using UnityEngine;
using UniRx;
using System.Collections;
using RabbitResurrection;
using TMPro;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using BBGamejam.Global.Mode;

public class Zara : MonoBehaviour
{
    private Animator animator;
    private SkinnedMeshRenderer rend;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int health;
    public GameObject Seat;
    public GameObject AirPocket;

    public float progress;

    private void Awake()
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponentInChildren<Animator>();    
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Move();
    }

    private void Init()
    {
        transform.position = _startPoint.transform.position;

        //for (int i = 0; i < health; i++)
        //{
        //    ((UI_InGameScene)Managers.UI.SceneUI).AddZaraHealth();
        //}
        ((UI_InGameScene)Managers.UI.SceneUI).SetZaraHealth(health);
    }

    public void SetData(GameObject startPoint, GameObject endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
    }

    private void Move()
    {
        var direction = _endPoint.transform.position - transform.position;
        transform.forward = direction;
        transform.position += direction.normalized * _speed * Time.deltaTime;
    }

    public float CalculateProgress()
    {
        var distanceToGoal = Vector3.Distance(transform.position, _endPoint.transform.position);
        var wholeDistance = Vector3.Distance(_startPoint.transform.position, _endPoint.transform.position);
        return Mathf.Clamp01(1f - (distanceToGoal / wholeDistance));
    }

    public void Damaged()
    {
        if (health > 0)
        {
            animator.SetTrigger("isHit");
            SlowModeManager.Instance.PlayLandingSound();
            HitAsync().Forget();
            (Managers.UI.SceneUI as UI_InGameScene).DamageZaraHealth();
            health--;

            if(health == 0)
            {
                Managers.Game.GameOver();
            }
        }
    }

    public async UniTask HitAsync()
    {
        await rend.material.DOColor(Color.red, "_EmissionColor", 0.2f);
        await rend.material.DOColor(Color.white, "_EmissionColor", 0.2f);
    }
}
