using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CurrentTime_TestTask
{
    public class ClockEditor : MonoBehaviour
    {
        [SerializeField] private Transform hourHand;
        [SerializeField] private Transform minuteHand;
        [SerializeField] private TimeService _timeService;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        private Transform _selectedHand;
        private bool _isInEditMode = false;

        private void Start()
        {
            if (_timeService == null)
            {
                Debug.LogError("TimeService не найден на сцене.");
            }
        }

        private void Update()
        {
            if (_isInEditMode)
            {
                HandleMouseInput();
            }
        }

        public void EnterEditMode()
        {
            _isInEditMode = true;
        }

        public void SaveEditedTime()
        {
            _isInEditMode = false;
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckHandSelection();
            }

            if (_selectedHand != null && Input.GetMouseButton(0))
            {
                RotateSelectedHand();
            }

            if (Input.GetMouseButtonUp(0))
            {
                SaveManualTime();
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
                if (result.gameObject == hourHand.gameObject)
                {
                    _selectedHand = hourHand;
                    break;
                }

                if (result.gameObject == minuteHand.gameObject)
                {
                    _selectedHand = minuteHand;
                    break;
                }
            }
        }

        private void RotateSelectedHand()
        {
            Vector2 screenPosition = Input.mousePosition;
            Vector2 handScreenPosition = _selectedHand.parent.position;

            Vector2 direction = screenPosition - handScreenPosition;
            float newAngle = Vector2.SignedAngle(Vector2.up, direction);

            _selectedHand.parent.rotation = Quaternion.Euler(0, 0, newAngle);

            UpdateTimeBasedOnHands();
        }

        private void UpdateTimeBasedOnHands()
        {
            float hourAngle = 360 - hourHand.parent.eulerAngles.z;
            float minuteAngle = 360 - minuteHand.parent.eulerAngles.z;

            DateTime currentTime = _timeService.GetCurrentTime();

            if (_selectedHand == hourHand)
            {
                UpdateHourHand(hourAngle, minuteAngle, currentTime);
            }
            else if (_selectedHand == minuteHand)
            {
                UpdateMinuteHand(minuteAngle, currentTime);
            }
        }

        private void UpdateHourHand(float hourAngle, float minuteAngle, DateTime currentTime)
        {
            int hours = hourAngle.GetHoursFromAngle();
            float hourFraction = (hourAngle % 30) / 30;
            int minutes = Mathf.FloorToInt(hourFraction * 60);
            
            int newHour = (hours + (minuteAngle >= 0 ? 0 : 1)) % 12;
            int hourAdjustment = 0;

            if (newHour == 0) newHour = 12; 
            
            if (currentTime.Hour >= 12)
            {
                hourAdjustment = (hours >= 12) ? 0 : 12;
            }
            else 
            {
                hourAdjustment = (hours >= 12) ? -12 : 0; 
            }

            newHour = (newHour + hourAdjustment) % 24;

            DateTime newTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
                newHour, minutes, currentTime.Second);

            _timeService.SetCurrentTime(newTime);

            minuteHand.parent.rotation = Quaternion.Euler(0, 0, minutes.GetMinuteHandAngle());
        }

        private void UpdateMinuteHand(float minuteAngle, DateTime currentTime)
        {
            int minutes = minuteAngle.GetMinutesFromAngle();
            int previousMinutes = currentTime.Minute;
            int hoursChange = 0;
            
            if (previousMinutes > 45 && minutes < 15)
            {
                hoursChange = 1;
            }
            else if (previousMinutes < 15 && minutes > 45)
            {
                hoursChange = -1;
            }

            int newHour = currentTime.Hour + hoursChange;
            
            newHour = ClockExtensions.NormalizeHour(newHour);

            DateTime newTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
                newHour, minutes, currentTime.Second);

            _timeService.SetCurrentTime(newTime);

            hourHand.parent.rotation = Quaternion.Euler(0, 0, newHour.GetHourHandAngle(minutes));
        }

        private void SaveManualTime()
        {
            _selectedHand = null;
        }
    }
}