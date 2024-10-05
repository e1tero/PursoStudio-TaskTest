using System;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class ClockEditorManager : IInitializable, IDisposable, IPauseListener
    {
        private readonly ClockInputHandler _clockInputHandler;
        private readonly ClockAngleToTimeConverter _clockAngleToTimeConverter;

        [Inject]
        public ClockEditorManager(ClockInputHandler clockInputHandler, ClockAngleToTimeConverter clockAngleToTimeConverter)
        {
            _clockInputHandler = clockInputHandler;
            _clockAngleToTimeConverter = clockAngleToTimeConverter;
        }

        public void Initialize()
        {
            _clockInputHandler.OnHandSelected += OnHandSelected;
            _clockInputHandler.OnHandRotated += OnHandRotated;
        }

        public void Dispose()
        {
            _clockInputHandler.OnHandSelected -= OnHandSelected;
            _clockInputHandler.OnHandRotated -= OnHandRotated;
        }

        public void OnPause()
        {
            _clockInputHandler.ExitEditMode();
        }

        public void OnResume()
        {
            _clockInputHandler.EnterEditMode();
        }

        private void OnHandSelected(HandType handType)
        {
            // В будущем можно добавить подсветку для выбранной стрелки
        }

        private void OnHandRotated(HandType handType, float angle)
        {
            _clockAngleToTimeConverter.UpdateTimeBasedOnHands(handType, angle);
        }
    }
}