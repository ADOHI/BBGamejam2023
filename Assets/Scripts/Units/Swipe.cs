using UnityEngine;

namespace RabbitResurrection
{
    public class Swipe : MonoBehaviour
    {
        [SerializeField] protected Trajectory trajectory;
        [SerializeField] protected float pushForce;
        [SerializeField] protected float maxForce;
        [SerializeField] private Rabbit Rabbit;
        private bool isFirstPressed = true;
        private bool isLastPressed = false;

        private Vector2 _startPosition, _endPosition;
        private Vector3 _forceVector;

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
            switch (mouseEvent)
            {
                case Define.MouseEvent.Press:
                    if (isFirstPressed)
                    {
                        isFirstPressed = false;
                        isLastPressed = true;

                        trajectory.Show();

                        _startPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    }
                    else
                    {
                        _endPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                        Vector3 direction = (_startPosition - _endPosition).normalized;
                        float distance = Vector2.Distance(_startPosition, _endPosition);

                        _forceVector = direction * distance * pushForce;
                        _forceVector.z = 0;
                        _forceVector = Vector3.ClampMagnitude(_forceVector, maxForce);

                        float drag = Rabbit.GetComponent<Rigidbody>().drag;
                        trajectory.UpdateDots(transform.position, _forceVector, drag);
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

                        trajectory.Hide();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
