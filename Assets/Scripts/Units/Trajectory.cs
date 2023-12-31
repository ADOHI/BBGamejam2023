using Dweiss;
using UnityEngine;

namespace RabbitResurrection
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] protected Transform dotsParent;
        [SerializeField] protected Transform dotPrefab;
        [SerializeField] protected Transform target;
        [SerializeField] protected int dotsNumber;
        [SerializeField] protected float dotSpacing;
        [SerializeField][Range(0.01f, 0.5f)] protected float dotMinScale;
        [SerializeField][Range(0.50f, 1.0f)] protected float dotMaxScale;

        private Transform[] _dotsList;

        private void Start()
        {
            Hide();

            PrepareDots();
        }

        private void PrepareDots()
        {
            _dotsList = new Transform[dotsNumber];
            dotPrefab.localScale = Vector3.one * dotMaxScale;

            //float scale = dotMaxScale;
            //float scaleFactor = scale / dotsNumber;

            for (int i = 0; i < dotsNumber; i++)
            {
                _dotsList[i] = Instantiate(dotPrefab, null);
                _dotsList[i].parent = dotsParent.transform;
                _dotsList[i].gameObject.SetActive(true);

                //_dotsList[i].localScale = Vector3.one * scale;

                /*if (scale > dotMinScale)
                {
                    scale -= scaleFactor;
                }*/
            }
        }

        public void UpdateDots(Rigidbody rb, Vector3 rabbitPosition, Vector3 forceApplied, float drag)
        {
            //var positions = GetTrajectoryPredictionPoints(rabbitPosition, forceApplied, drag, dotsNumber, Time.fixedDeltaTime);

            /*dotsParent.transform.position = rabbitPosition;

            float timeStamp = dotSpacing;

            dotsParent.transform.position = Camera.main.WorldToScreenPoint(rabbitPosition);*/
            dotsParent.transform.position = Vector3.zero;

            var timeBetweenStep = (int)(5 / Time.deltaTime);

            var positions = rb.CalculateMovement(dotsNumber, 5, forceApplied);

            for (int i = 0; i < _dotsList.Length; i++)
            {
                _dotsList[i].transform.position = positions[i];
                _dotsList[i].LookAt(Camera.main.transform);
            }
/*            foreach (var dot in _dotsList)
            {
                Vector3 dotPos;

                dotPos = rabbitPosition + (forceApplied * timeStamp);
                dotPos.z = 0f;

                dot.position = dotPos;
                dot.LookAt(Camera.main.transform);
                timeStamp += dotSpacing;
            }*/
        }

        public void Show()
        {
            dotsParent.gameObject.SetActive(true);
        }

        public void Hide()
        {
            dotsParent.gameObject.SetActive(false);
        }

    }
}
