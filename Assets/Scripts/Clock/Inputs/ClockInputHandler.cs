using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask
{
    public class ClockInputHandler : ITickable
    {
        private readonly GraphicRaycaster _graphicRaycaster;
        private readonly IClockHandsProvider _clockHandsProvider;
        private HandType? _selectedHand;
        private bool _isInEditMode;

        public event Action<HandType> OnHandSelected;
        public event Action<HandType, float> OnHandRotated;

        public ClockInputHandler(GraphicRaycaster graphicRaycaster, IClockHandsProvider clockHandsProvider)
        {
            _graphicRaycaster = graphicRaycaster;
            _clockHandsProvider = clockHandsProvider;
        }

        public void EnterEditMode()
        {
            _isInEditMode = true;
        }

        public void ExitEditMode()
        {
            _isInEditMode = false;
        }

        public void Tick()
        {
            if (_isInEditMode)
            {
                HandleMouseInput();
            }
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckHandSelection();
            }

            if (_selectedHand.HasValue && Input.GetMouseButton(0))
            {
                RotateSelectedHand(_selectedHand.Value);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _selectedHand = null;
            }
        }

        private void CheckHandSelection()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.transform == _clockHandsProvider.GetHourHand())
                {
                    _selectedHand = HandType.Hour;
                }
                else if (result.gameObject.transform == _clockHandsProvider.GetMinuteHand())
                {
                    _selectedHand = HandType.Minute;
                }

                if (_selectedHand.HasValue)
                {
                    OnHandSelected?.Invoke(_selectedHand.Value);
                    break;
                }
            }
        }

        private void RotateSelectedHand(HandType handType)
        {
            Transform hand = handType == HandType.Hour
                ? _clockHandsProvider.GetHourHand()
                : _clockHandsProvider.GetMinuteHand();
            Vector2 screenPosition = Input.mousePosition;
            Vector2 handScreenPosition = hand.parent.position;

            Vector2 direction = screenPosition - handScreenPosition;
            float newAngle = Vector2.SignedAngle(Vector2.up, direction);

            hand.parent.rotation = Quaternion.Euler(0, 0, newAngle);
            OnHandRotated?.Invoke(handType, hand.eulerAngles.z);
        }
    }
}