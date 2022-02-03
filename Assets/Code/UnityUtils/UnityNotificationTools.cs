using System;
using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif

namespace Code.UnityUtils
{
    public sealed class UnityNotificationTools
    {
        private const string AndroidNotifierId = "dsvinka_racinggame2d_id";

        public void CreateNotification(string name, string description, Importance importance)
        {
#if UNITY_ANDROID
            var androidSettingsChanel = new AndroidNotificationChannel
            {
                Id = AndroidNotifierId,
                Name = name,
                Importance = importance,
                CanBypassDnd = true,
                CanShowBadge = true,
                Description = description,
                EnableLights = true,
                EnableVibration = true,
                LockScreenVisibility = LockScreenVisibility.Public
            };
      
            AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChanel);
      
            var androidSettingsNotification = new AndroidNotification
            {
                Color = Color.white,
                RepeatInterval = TimeSpan.FromSeconds(5)
            };

            AndroidNotificationCenter.SendNotification(androidSettingsNotification, AndroidNotifierId);
#elif UNITY_IOS
            var iosSettingsNotification = new iOSNotification
            {
                Identifier = AndroidNotifierId,
                Title = name,
                Subtitle = name,
                Body = description,
                Badge = 1,
                Data = DateTime.Today.ToShortDateString(),
                ForegroundPresentationOption = PresentationOption.Alert,
                ShowInForeground = false
            };

            iOSNotificationCenter.ScheduleNotification(iosSettingsNotification);
#endif
        }
    }
}