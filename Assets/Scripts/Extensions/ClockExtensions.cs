using System.Linq;
using UnityEngine;

namespace CurrentTime_TestTask
{
    public static class ClockExtensions
    {
        private const float HourDegree = 30f;
        private const float MinuteDegree = 6f;
        private const int MinutesInHour = 60;
        private const int HoursInHalfDay = 12;
        private const int HoursInDay = 24;

        public static string FormatToTime(this string input)
        {
            string digits = new string(input.Where(char.IsDigit).ToArray());

            if (digits.Length >= 4)
            {
                return digits.Insert(2, ":").Substring(0, 5);
            }
            else if (digits.Length >= 2)
            {
                return digits.Insert(2, ":");
            }

            return digits;
        }
        public static int GetHoursFromAngle(this float angle)
        {
            return Mathf.FloorToInt(angle / HourDegree);
        }

        public static int GetMinutesFromAngle(this float angle)
        {
            return Mathf.FloorToInt(angle / MinuteDegree);
        }

        public static float GetHourHandAngle(this int hours, int minutes)
        {
            return -(hours % HoursInHalfDay * HourDegree + (minutes / (float)MinutesInHour) * HourDegree);
        }

        public static float GetMinuteHandAngle(this int minutes)
        {
            return -(minutes * MinuteDegree);
        }
        
        public static int NormalizeHour(int hour)
        {
            if (hour < 0) return hour + HoursInDay;
            return hour % HoursInDay;
        }
    }
}