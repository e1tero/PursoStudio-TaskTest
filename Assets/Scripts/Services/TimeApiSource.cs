using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

namespace CurrentTime_TestTask
{
    public class TimeApiSource : ITimeSource
    {
        private const string ApiUrl = "https://www.timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow";

        public IEnumerator GetCurrentTime(Action<DateTime> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(ApiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var jsonResult = request.downloadHandler.text;
                var apiTime = JsonUtility.FromJson<ApiTimeResponse>(jsonResult);
                if (DateTime.TryParse(apiTime.dateTime, out var currentTime))
                {
                    callback?.Invoke(currentTime);
                }
                else
                {
                    Debug.LogError("Ошибка парсинга времени из API");
                    callback?.Invoke(DateTime.MinValue);
                }
            }
            else
            {
                Debug.LogError("Ошибка получения времени: " + request.error);
                callback?.Invoke(DateTime.MinValue);
            }
        }

        [Serializable]
        private class ApiTimeResponse
        {
            public string dateTime;
        }
    }
}
