﻿namespace Code.Analytics
{
    public interface IAnalyticsTools
    {
        void SendMessage(string eventName);
        void SendMessage(string eventName, (string key, object value) data);
    }
}