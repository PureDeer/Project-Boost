using UnityEngine;

namespace ProjectBoost
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private float _thrust = 300.0f;
        [SerializeField] private float _rotationT = 300.0f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ProcessRotation();
        }

        private void FixedUpdate()
        {
            ProcessThrust();
        }

        private void ProcessThrust()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddRelativeForce(Vector3.up * _thrust * Time.deltaTime);
            }
        }

        private void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A))
            {
                ApplyRotation(_rotationT);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ApplyRotation(-_rotationT);
            }
        }

        private void ApplyRotation(float thrust)
        {
            // 避免撞到其他Rigidbody后使得物体无法操控
            _rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * thrust * Time.deltaTime);
            _rb.freezeRotation = false;
        }
    }
}