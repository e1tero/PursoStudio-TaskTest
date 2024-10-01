using UnityEngine;

namespace PlayphoriaPack
{
    public enum ClickType { None, Left, Right, Both}

    public class VideoHand : MonoBehaviour
    {
        [SerializeField] private ClickType particleMouseClick;
        [SerializeField] private FadableUI touchParticle;
        [SerializeField] private Transform particleParent;
        [SerializeField] private ClickType rotationMouseClick;
        [SerializeField] private float rotatationTime = 0.125f;
        [SerializeField] private Vector3 startRotation;
        [SerializeField] private Vector3 endRotation;

        private Quaternion startQuaternion;
        private Quaternion endQuaternion;
        private float time = 0;
        
        private int buttonDown = -1;
        private int buttonUp = -1;
        private bool flag = false;
        
        private void Awake()
        {
            startQuaternion = Quaternion.Euler(startRotation);
            endQuaternion = Quaternion.Euler(endRotation);
        }
        
        private void Update()
        {
            // Button events check
            if (Input.GetMouseButtonDown(0)) buttonDown = 0;
            if (Input.GetMouseButtonDown(1)) buttonDown = 1;
            if (Input.GetMouseButtonUp(0)) buttonUp = 0;
            if (Input.GetMouseButtonUp(1)) buttonUp = 1;

            // Click settings check
            if (CheckButton(buttonDown, particleMouseClick)) SpawnTouchParticle();
            if (CheckButton(buttonDown, rotationMouseClick)) flag = true;
            if (CheckButton(buttonUp, rotationMouseClick)) flag = false;

            // Time
            float delta = (flag ? Time.deltaTime : -Time.deltaTime) / rotatationTime;
            time = Mathf.Clamp01(time + delta);
            
            // Set
            transform.position = Input.mousePosition;
            transform.rotation = Quaternion.Lerp(startQuaternion, endQuaternion, time);

            // Reset
            buttonDown = -1;
            buttonUp = -1;
        }

        private bool CheckButton(int button, ClickType clickType)
        {
            return 
                button == 0 && clickType is ClickType.Left or ClickType.Both
                || 
                button == 1 && clickType is ClickType.Right or ClickType.Both;
        }
        
        private void SpawnTouchParticle() => Instantiate(touchParticle, transform.position, Quaternion.identity, particleParent);
    }
}