using System;
using UnityEngine;

namespace Code.Repositories.Models
{
    public sealed class RewardSaveModel
    {
        public static string CurrentRewardSlotKey = nameof(CurrentRewardSlotKey);
        public static string TimeToRewardKey = nameof(TimeToRewardKey);
        
        public int CurrentRewardSlot
        {
            get => PlayerPrefs.GetInt(CurrentRewardSlotKey, 0);
            set => PlayerPrefs.SetInt(CurrentRewardSlotKey, value);
        }

        public DateTime? TimeToReward
        {
            get
            {
                var data = PlayerPrefs.GetString(TimeToRewardKey, null);
                if (!string.IsNullOrEmpty(data))
                    return DateTime.Parse(data);

                return null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeToRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeToRewardKey);
            }
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(TimeToRewardKey);
            PlayerPrefs.DeleteKey(CurrentRewardSlotKey);
        }
    }
}