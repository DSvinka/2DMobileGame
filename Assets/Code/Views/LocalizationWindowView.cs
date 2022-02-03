using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Code.Views
{
    public sealed class LocalizationWindowView: MonoBehaviour
    {
        [SerializeField] private Button _russianTranslateButton;
        [SerializeField] private Button _englishTranslateButton;

        private const int RussianLocalizationIndex = 1;
        private const int EnglishLocalizationIndex = 0;

        private void Start()
        {
            _russianTranslateButton.onClick.AddListener(() => ChangeLocale(RussianLocalizationIndex));
            _englishTranslateButton.onClick.AddListener(() => ChangeLocale(EnglishLocalizationIndex));
        }

        private void OnDestroy()
        {
            _russianTranslateButton.onClick.RemoveAllListeners();
            _englishTranslateButton.onClick.RemoveAllListeners();
        }

        private void ChangeLocale(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}