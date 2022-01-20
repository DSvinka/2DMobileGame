using System;
using Code.Types;
using UnityEngine;

namespace Code.Repositories
{
    public sealed class SavesRepository: BaseController
    {
        private readonly SaveRewardModel _saveRewardModel;
        private readonly SaveCurrencyModel _saveCurrencyModel;
        
        public SaveRewardModel SaveRewardModel => _saveRewardModel;
        public SaveCurrencyModel SaveCurrencyModel => _saveCurrencyModel;
        
        public SavesRepository()
        {
            _saveRewardModel = new SaveRewardModel();
            _saveCurrencyModel = new SaveCurrencyModel();
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public sealed class SaveCurrencyModel
    {
        public static string CurrencyWoodCountKey = nameof(CurrencyWoodCountKey);
        public static string CurrencyMetalCountKey = nameof(CurrencyMetalCountKey);
        public static string CurrencyMoneyCountKey = nameof(CurrencyMoneyCountKey);

        public event Action<CurrencyType> OnCurrencyCountChange;
        
        public int CurrencyWoodCount
        {
            get => PlayerPrefs.GetInt(CurrencyWoodCountKey, 0);
            set
            {
                PlayerPrefs.SetInt(CurrencyWoodCountKey, value);
                OnCurrencyCountChange?.Invoke(CurrencyType.Wood);
            }
        }

        public int CurrencyMetalCount
        {
            get => PlayerPrefs.GetInt(CurrencyMetalCountKey, 0);
            set
            {
                PlayerPrefs.SetInt(CurrencyMetalCountKey, value);
                OnCurrencyCountChange?.Invoke(CurrencyType.Metal);
            }
        }

        public int CurrencyMoneyCount
        {
            get => PlayerPrefs.GetInt(CurrencyMoneyCountKey, 0);
            set
            {
                PlayerPrefs.SetInt(CurrencyMoneyCountKey, value);
                OnCurrencyCountChange?.Invoke(CurrencyType.Money);
            }
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(CurrencyWoodCountKey);
            PlayerPrefs.DeleteKey(CurrencyMetalCountKey);
            PlayerPrefs.DeleteKey(CurrencyMoneyCountKey);
        }
    }

    public sealed class SaveRewardModel
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