using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class ClockView : IInitializable, IDisposable
    {
        private readonly Transform _hourHand;
        private readonly Transform _minuteHand;
        private readonly Transform _secondHand;
        private readonly ITimeUpdater _timeUpdater;
        
        public ClockView(Transform hourHand, Transform minuteHand, Transform secondHand, ITimeUpdater timeUpdater)
        {
            _hourHand = hourHand;
            _minuteHand = minuteHand;
            _secondHand = secondHand;
            _timeUpdater = timeUpdater;
        }
        
        public void Initialize()
        {
            _timeUpdater.OnTimeUpdated += RotateHands;
        }
        
        public void Dispose()
        {
            _timeUpdater.OnTimeUpdated -= RotateHands;
        }

        private void RotateHands(float hours, float minutes, float seconds, bool isInstantly)
        {
            if (!isInstantly)
            {
                _secondHand.DOLocalRotate(new Vector3(0, 0, -seconds * 6f), 1f).SetEase(Ease.Linear);
                _minuteHand.DOLocalRotate(new Vector3(0, 0, -minutes * 6f), 1f).SetEase(Ease.Linear);
                _hourHand.DOLocalRotate(new Vector3(0, 0, -hours * 30f), 1f).SetEase(Ease.Linear);
            }
            else
            {
                _secondHand.rotation = Quaternion.Euler(0, 0, -seconds * 6f);
                _minuteHand.rotation = Quaternion.Euler(0, 0, -minutes * 6f);
                _hourHand.rotation = Quaternion.Euler(0, 0, -hours * 30f);
            }
        }
        
    }
}