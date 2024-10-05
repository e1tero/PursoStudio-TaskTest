using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask
{
    public class BackgroundColorChanger : IPauseListener, IInitializable, IDisposable
    {
        private readonly Image _backgroundImage;
        private readonly Color _pausedColor;
        private readonly Color _defaultColor;
        private readonly float _changeDuration;
        private readonly PauseObserver _pauseObserver;

        public BackgroundColorChanger(Image backgroundImage, Color pausedColor, Color defaultColor, float changeDuration, PauseObserver  pauseObserver)
        {
            _backgroundImage = backgroundImage;
            _pausedColor = pausedColor;
            _defaultColor = defaultColor;
            _changeDuration = changeDuration;
            _pauseObserver = pauseObserver;
        }
        
        public void Initialize()
        {
            _pauseObserver.OnPause += OnPause;
            _pauseObserver.OnResume += OnResume;
        }
        public void Dispose()
        {
            _pauseObserver.OnPause -= OnPause;
            _pauseObserver.OnResume -= OnResume;
        }
        private void ChangeToPausedColor()
        {
            _backgroundImage.DOColor(_pausedColor, _changeDuration);
        }

        private void ChangeToDefaultColor()
        {
            _backgroundImage.DOColor(_defaultColor, _changeDuration).SetEase(Ease.OutBounce);
        }

        public void OnPause()
        {
            ChangeToPausedColor();
        }

        public void OnResume()
        {
            ChangeToDefaultColor();
        }
    }
}