using UnityEngine;

namespace ProjectBoost
{
    public class Oscillator : MonoBehaviour
    {
        private Vector3 _startPos;
        private float _movementFactor;

        [SerializeField] private Vector3 _movementVec = Vector3.zero;
        [SerializeField] private float _period = 2f;
        [SerializeField] private bool _isLocalOscillate = false;

        private const float Tau = Mathf.PI * 2;

        private void Start()
        {
            _startPos = _isLocalOscillate ? transform.localPosition : transform.position;
        }

        private void Update()
        {
            if (_isLocalOscillate) transform.localPosition = _startPos + Oscillate();
            else transform.position = _startPos + Oscillate();
        }

        private Vector3 Oscillate()
        {
            // 避免零除
            if (_period <= Mathf.Epsilon) return Vector3.zero;
            // 有多少个周期
            var cycles = Time.time / _period;
            // 一个周期长度
            // 周期数 * 单位周期长度 = 角度
            // (结果在-1 到 1 之间变化)
            // 角度越大，变化越快
            var rawSinWave = Mathf.Sin(cycles * Tau);
            // 把值限制在0 ~ 1
            _movementFactor = (rawSinWave + 1) / 2;

            return _movementVec * _movementFactor;
        }
    }
}