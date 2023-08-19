using Cinemachine;
using UnityEngine;

namespace RabbitResurrection
{
    public class CineTarget : MonoBehaviour
    {
        private CinemachineFramingTransposer transposer;
        [SerializeField] Rabbit rabbit;
        [SerializeField] Zara zara;
        [SerializeField] CinemachineVirtualCamera cine;
        [SerializeField] float initialDistance;

        public Vector3 targetOffset;
        public void SetData(Rabbit rabbit, Zara zara, CinemachineVirtualCamera cine, Vector3 offset)
        {
            this.rabbit = rabbit;
            this.zara = zara;
            this.cine = cine;
            this.targetOffset = offset;
            Vector3 zeroWorld = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector3 oneWorld = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            initialDistance = Vector3.Distance(zeroWorld, oneWorld);

            transposer = cine.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void FixedUpdate()
        {
            transform.position = (rabbit.transform.position + zara.AirPocket.transform.position) * 0.5f + targetOffset;
            var position = transform.position;
            position.z = 0f;
            transform.position = position;

            //if (IsRabbitOutView() || IsZaraOutView())
            //{
            //    cine.m_Lens.FocusDistance += 1f;
            //    cine.
            //}
            //else
            //{
            //    if(Vector3.Distance(rabbit.transform.position, zara.AirPocket.transform.position) < initialDistance)
            //    {
            //        cine.m_Lens.FocusDistance -= 1f;
            //    }
            //}
        }

        public void SetBodyDistance(float distance)
        {
            transposer.m_CameraDistance = distance;
        }

        private bool IsRabbitOutView()
        {
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(rabbit.transform.position);

            if (viewportPosition.x < 0f || viewportPosition.x > 1f ||
            viewportPosition.y < 0f || viewportPosition.y > 1f ||
            viewportPosition.z < 0f)
            {
                return true;
            }

            return false;
        }

        private bool IsZaraOutView()
        {
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(zara.AirPocket.transform.position);

            if (viewportPosition.x < 0f || viewportPosition.x > 1f ||
            viewportPosition.y < 0f || viewportPosition.y > 1f ||
            viewportPosition.z < 0f)
            {
                return true;
            }

            return false;
        }
    }
}