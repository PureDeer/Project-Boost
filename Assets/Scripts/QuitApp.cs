using UnityEditor;
using UnityEngine;

namespace ProjectBoost
{
    public class QuitApp : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }
    }
}