using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CurrentTime_TestTask
{
    public class BackgroundColorChanger : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage; 
        [SerializeField] private Color _pausedColor;
        [SerializeField] private Color _defaultColor;

        [SerializeField] private float _changeDuration;

        public void ChangeToPausedColor()
        {
            _backgroundImage.DOColor(_pausedColor, _changeDuration);
        }

        public void ChangeToDefaultColor()
        {
            _backgroundImage.DOColor(_defaultColor, _changeDuration).SetEase(Ease.OutBounce);
        }
    }
}