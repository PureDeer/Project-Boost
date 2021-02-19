using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectBoost
{
    public class CollisionHandler : MonoBehaviour
    {
        private int _totalScene;
        private Movement _movement;
        private AudioSource _as;
        private bool _isTrans;
        [SerializeField] private float _delayTime = 1.5f;

        [SerializeField] private AudioClip _crashAudio = null;
        [SerializeField] private AudioClip _successAudio = null;

        [SerializeField] private ParticleSystem _crashParticle = null;
        [SerializeField] private ParticleSystem _successParticle = null;

        private void Awake()
        {
            _as = GetComponent<AudioSource>();
            _totalScene = SceneManager.sceneCountInBuildSettings;
            _movement = GetComponent<Movement>();
        }

        private void OnCollisionEnter(Collision other)
        {
            // 如果处于以下的某个状态中，那么就不继续进行碰撞
            if (_isTrans) return;
            var colTag = other.gameObject.tag;

            switch (colTag)
            {
                case "Friendly":
                    Debug.Log("Hit Friendly");
                    break;

                case "Fuel":
                    Debug.Log("Hit Fuel");
                    break;

                case "Finished":
                    StartSuccess();
                    break;

                default:
                    StartCrash();
                    break;
            }
        }

        private void StartSuccess()
        {
            // 标记为正在进行状态
            _isTrans = true;
            // 进入状态时。停止前一个状态的声音
            _as.Stop();
            // 播放指定音效
            _as.PlayOneShot(_successAudio);
            // 播放粒子特效
            _successParticle.Play();
            _delayTime = 4.0f;
            _movement.enabled = false;
            Invoke(nameof(LoadNextLevel), _delayTime);
        }

        private void StartCrash()
        {
            _isTrans = true;
            _as.Stop();
            _as.PlayOneShot(_crashAudio);
            _crashParticle.Play();
            _delayTime = 2.0f;
            _movement.enabled = false;
            Invoke(nameof(ReloadLevel), _delayTime);
        }

        private void ReloadLevel()
        {
            var curIdx = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(curIdx);
            _delayTime = 1.5f;
        }

        private void LoadNextLevel()
        {
            var curIdx = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++curIdx % _totalScene);
            _delayTime = 1.5f;
        }
    }
}