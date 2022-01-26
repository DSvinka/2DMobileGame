using System;
using Code.Enums;
using UnityEngine;

namespace Code.Repositories
{
    public sealed class SavesRepository: BaseController
    {
        private readonly SaveCarModel _saveCarModel;
        private readonly SaveRewardModel _saveRewardModel;
        private readonly SaveCurrencyModel _saveCurrencyModel;

        public SaveCarModel SaveCarModel => _saveCarModel;
        public SaveRewardModel SaveRewardModel => _saveRewardModel;
        public SaveCurrencyModel SaveCurrencyModel => _saveCurrencyModel;
        
        public SavesRepository()
        {
            _saveCarModel = new SaveCarModel();
            _saveRewardModel = new SaveRewardModel();
            _saveCurrencyModel = new SaveCurrencyModel();
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public sealed class SaveCarModel
    {
        public static string HealthKey = nameof(HealthKey);
        public static string BulletDamageKey = nameof(BulletDamageKey);
        public static string BulletShotRateKey = nameof(BulletShotRateKey);
        public static string BulletShotForceKey = nameof(BulletShotForceKey);
        
        public float Health
        {
            get => PlayerPrefs.GetFloat(HealthKey, 100);
            set => PlayerPrefs.SetFloat(HealthKey, value);
        }

        public float BulletDamage
        {
            get => PlayerPrefs.GetFloat(BulletDamageKey, 10);
            set => PlayerPrefs.SetFloat(BulletDamageKey, value);
        }

        public float BulletShotRate
        {
            get => PlayerPrefs.GetFloat(BulletShotRateKey, 0.5f);
            set => PlayerPrefs.SetFloat(BulletShotRateKey, value);
        }
        
        public float BulletShotForce
        {
            get => PlayerPrefs.GetFloat(BulletShotForceKey, 15f);
            set => PlayerPrefs.SetFloat(BulletShotForceKey, value);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(HealthKey);
            PlayerPrefs.DeleteKey(BulletDamageKey);
            PlayerPrefs.DeleteKey(BulletShotRateKey);
            PlayerPrefs.DeleteKey(BulletShotForceKey);
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