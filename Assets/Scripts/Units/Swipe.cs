using BBGamejam.Global.Ingame;
using BBGamejam.Ingame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public class Swipe : MonoBehaviour
    {
        [SerializeField] protected Trajectory trajectory;
        [SerializeField] public float maxForce;
        //[SerializeField] protected float maxForce;
        [SerializeField] private Rabbit Rabbit;
        private bool isFirstPressed = true;
        private bool isLastPressed = false;

        private Vector2 _startPosition, _endPosition;
        private Vector3 _forceVector;

        private Animator animator => Rabbit.animator;

        [Header("SwipeSetting")]
        public float maxSwipeDistance;

        [Header("SwipeUI")]
        public CanvasScaler scaler;
        public GameObject swipeUIParent;
        public Image centerImage;
        public Image outerCircleImage;
        public Image mousePointerImage;
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            Managers.Input.MouseAction -= ControlSwipe;
            Managers.Input.MouseAction += ControlSwipe;
        }

        private void ControlSwipe(Define.MouseEvent mouseEvent)
        {
            if (IngameManager.Instance.isGamePause)
            {
                return;
            }

            switch (mouseEvent)
            {
                case Define.MouseEvent.Press:
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isReady", true);
                    float mouseRatioX = Input.mousePosition.x / Screen.height;
                    float mouseRatioY = Input.mousePosition.y / Screen.height;
                    if (isFirstPressed)
                    {
                        //IngameUIManager.Instance.HideComboText();
                        isFirstPressed = false;
                        isLastPressed = true;
                        swipeUIParent.SetActive(true);
                        trajectory.Show();                        
                        _startPosition = new Vector2(mouseRatioX, mouseRatioY);
                        var screenPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                        screenPosition.Scale(scaler.referenceResolution);
                        centerImage.transform.position = screenPosition;
                        outerCircleImage.transform.position = screenPosition;
                        //_startPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    }
                    else
                    {
                        _endPosition = new Vector2(mouseRatioX, mouseRatioY);
                        //var screenPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                        //screenPosition.Scale(scaler.referenceResolution);
                        //_endPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                        Vector2 direction = (_startPosition - _endPosition).normalized;
                        var distance = Vector2.Distance(_startPosition, _endPosition);
                        var clampedSwipeDistance = Mathf.Clamp(distance, 0f, maxSwipeDistance);

                        var clampedEndPosition = _startPosition + (clampedSwipeDistance * -direction);
                        clampedEndPosition.Scale(new Vector2(scaler.referenceResolution.y, scaler.referenceResolution.y));
                        mousePointerImage.transform.position = clampedEndPosition;

                        var circleSize = (clampedSwipeDistance * -direction);
                        circleSize.Scale(new Vector2(scaler.referenceResolution.y, scaler.referenceResolution.y) * 2);

                        outerCircleImage.rectTransform.sizeDelta = Vector2.one * maxSwipeDistance * scaler.referenceResolution.y * 2;
                        _forceVector = direction * clampedSwipeDistance / maxSwipeDistance * maxForce;
                        _forceVector.z = 0;
                        //_forceVector = Vector3.ClampMagnitude(_forceVector, maxForce);

                        //float drag = Rabbit.GetComponent<Rigidbody>().drag;

                        var rb = Rabbit.GetComponent<Rigidbody>();
                        trajectory.UpdateDots(rb, transform.position, _forceVector, rb.drag);
                    }
                    break;
                case Define.MouseEvent.Click:
                    if (isLastPressed)
                    {
                        isLastPressed = false;
                        isFirstPressed = true;

                        if (Rabbit)
                        {
                            Rabbit.Push(_forceVector);
                        }
                        swipeUIParent.SetActive(false);
                        trajectory.Hide();
                    }
                    break;
                default:
                    break;
            }
        }

        public void UpgradeSwipeForce()
        {
            maxForce += 1.1f;
        }

    }
}
