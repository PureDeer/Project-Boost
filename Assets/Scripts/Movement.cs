using UnityEngine;

namespace ProjectBoost
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;
        private AudioSource _as;
        [SerializeField] private float _mainThrust = 300.0f;
        [SerializeField] private float _rotationThrust = 300.0f;

        [SerializeField] private AudioClip _mainBoostAudio = null;

        [SerializeField] private ParticleSystem _mainBoostParticle = null;
        [SerializeField] private ParticleSystem _leftBoostParticle = null;
        [SerializeField] private ParticleSystem _rightBoostParticle = null;

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
            if (Input.GetKey(KeyCode.Space)) StartThrusting();
            else StopThrusting();
        }

        private void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A)) RotateLeft();
            else if (Input.GetKey(KeyCode.D)) RotateRight();
            else StopRotating();
        }

        private void StartThrusting()
        {
            // 相对于物体的位置施加力
            _rb.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
            // 如果没有音频在播放，则播放当前的音频
            if (!_as.isPlaying) _as.PlayOneShot(_mainBoostAudio);
            // 播放粒子动画
            if (!_mainBoostParticle.isPlaying) _mainBoostParticle.Play();
        }

        private void StopThrusting()
        {
            _as.Stop();
            _mainBoostParticle.Stop();
        }

        private void RotateLeft()
        {
            ApplyRotation(_rotationThrust);
            if (!_leftBoostParticle.isPlaying) _leftBoostParticle.Play();
        }

        private void RotateRight()
        {
            ApplyRotation(-_rotationThrust);
            if (!_rightBoostParticle.isPlaying) _rightBoostParticle.Play();
        }

        private void StopRotating()
        {
            _leftBoostParticle.Stop();
            _rightBoostParticle.Stop();
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