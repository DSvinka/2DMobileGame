using System.Collections.Generic;

namespace Code.Analytics
{
    public sealed class UnityAnalyticsTools: IAnalyticsTools
    {
        public void SendMessage(string eventName)
        {
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);
        }

        public void SendMessage(string eventName, (string key, object value) data)
        {
            var eventData = new Dictionary<string, object>
            {
                [data.key] = data.value
            };
            
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);
        }
    }
}