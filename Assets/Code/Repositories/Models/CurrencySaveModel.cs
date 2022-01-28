using System;
using Code.Enums;
using UnityEngine;

namespace Code.Repositories.Models
{
    public sealed class CurrencySaveModel
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
}