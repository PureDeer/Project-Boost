using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectBoost
{
    public class CollisionHandler : MonoBehaviour
    {
        private int _totalScene;
        private Movement _movement;
        private AudioSource _as;
        private bool _isTrans = false;
        [SerializeField] private float _delayTime = 1.5f;
        [SerializeField] private AudioClip _crash = null;
        [SerializeField] private AudioClip _success = null;

        private void Awake()
        {
            _as = GetComponent<AudioSource>();
            _totalScene = SceneManager.sceneCountInBuildSettings;
            _movement = GetComponent<Movement>();
        }

        private void OnCollisionEnter(Collision other)
        {
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
            _as.PlayOneShot(_success);
            _delayTime = 4.0f;
            _movement.enabled = false;
            Invoke(nameof(LoadNextLevel), _delayTime);
        }

        private void StartCrash()
        {
            _isTrans = true;
            _as.Stop();
            _as.PlayOneShot(_crash);
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