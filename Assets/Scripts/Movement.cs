using UnityEngine;

namespace ProjectBoost
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;
        private AudioSource _as;
        [SerializeField] private float _mainThrust = 300.0f;
        [SerializeField] private float _rotationThrust = 300.0f;
        [SerializeField] private AudioClip _mainEngine = null;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _as = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ProcessRotation();
            ProcessThrust();
        }

        private void ProcessThrust()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
                if (!_as.isPlaying)
                {
                    _as.PlayOneShot(_mainEngine);
                }
            }
            else
            {
                _as.Stop();
            }
        }

        private void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A))
            {
                ApplyRotation(_rotationThrust);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ApplyRotation(-_rotationThrust);
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