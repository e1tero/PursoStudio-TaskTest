using System.Collections;
using UnityEngine;

namespace CurrentTime_TestTask
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject runnerObject = new GameObject("CoroutineRunner");
                    _instance = runnerObject.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(runnerObject);
                }
                return _instance;
            }
        }

        public void StartRunnerCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}