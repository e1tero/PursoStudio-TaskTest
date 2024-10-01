using System.Collections;
using UnityEngine;

namespace PlayphoriaPack
{
    [RequireComponent(typeof(CanvasGroup), typeof(Animator))]
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private bool usePlayerPrefs;
        [SerializeField] private bool unskipableUntilShowed = true;
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float showUpTime = 1f;
        [SerializeField] private float fadeOutTime = 1f;
        [SerializeField] private float animationSpeed = 1f;

        private bool TutorialShown
        {
            get => PlayerPrefs.HasKey(nameof(TutorialShown));
            set => PlayerPrefs.SetInt(nameof(TutorialShown), 1);
        }

        private CanvasGroup canvasGroup;
        
        private IEnumerator Start()
        {
            if (!usePlayerPrefs) PlayerPrefs.DeleteKey(nameof(TutorialShown));
            
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            
            // Check if Tutorial is Shown
            if (TutorialShown) yield break;
            
            // Start Delay
            yield return new WaitForSeconds(startDelay);

            TutorialShown = true;
            GetComponent<Animator>().speed = animationSpeed;

            // Show
            while (canvasGroup.alpha < 1 && (unskipableUntilShowed || !Input.GetMouseButtonDown(0)))
            {
                canvasGroup.alpha += Time.deltaTime / showUpTime;
                yield return null;
            }

            // Wait for Input
            if (canvasGroup.alpha == 1) yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            
            // Hide
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeOutTime;
                yield return null;
            }
            
            // Dispose
            gameObject.SetActive(false);
        }
    }
}