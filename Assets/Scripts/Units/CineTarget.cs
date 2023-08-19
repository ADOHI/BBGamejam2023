using Cinemachine;
using UnityEngine;

namespace RabbitResurrection
{
    public class CineTarget : MonoBehaviour
    {
        [SerializeField] Rabbit rabbit;
        [SerializeField] Zara zara;
        [SerializeField] CinemachineVirtualCamera cine;

        public void SetData(Rabbit rabbit, Zara zara, CinemachineVirtualCamera cine)
        {
            this.rabbit = rabbit;
            this.zara = zara;
            this.cine = cine;
        }

        private void FixedUpdate()
        {
            transform.position = (rabbit.transform.position + zara.AirPocket.transform.position) / 2;
            var position = transform.position;
            position.z = 0f;
            transform.position = position;

            if (IsRabbitOutView() || IsZaraOutView())
            {
                cine.m_Lens.FocusDistance -= 5f;
            }
            else
            {

            }
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