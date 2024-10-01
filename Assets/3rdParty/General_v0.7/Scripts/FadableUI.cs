using UnityEngine;

namespace PlayphoriaPack
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadableUI : MonoBehaviour
    {
        [SerializeField] private float scale = 3f;
        [SerializeField] private float time = 0.5f;
        [SerializeField, Range(0f, 1f)] private float opaqueAt = 0.4f;
        
        private CanvasGroup canvasGroup;
        private float t = 0;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            
            transform.localScale = Vector3.zero;
            canvasGroup.alpha = 0;
        }

        private void Update()
        {
            t += Time.deltaTime;
            float factor = t / time;
                
            // Scale
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * scale, factor);
            
            // Alpha
            if (factor < opaqueAt)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, factor / opaqueAt);
            }
            else
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, (factor - opaqueAt) / (1 - opaqueAt));
            }
            
            // Dispose
            if (t > time) Destroy(gameObject);
        }
    }
    
}