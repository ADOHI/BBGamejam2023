using UnityEngine;

namespace RabbitResurrection
{
    public class Rabbit : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        private void Update()
        {
            if(_rigidbody.velocity.magnitude < 0.1f)
            {
                Debug.Log("Rabbit Stop");
            }
        }

        public void Push(Vector3 force)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_rigidbody.mass * force, ForceMode.Impulse);
        }
    }
}
