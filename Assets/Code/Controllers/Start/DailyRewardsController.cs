using System;
using System.Collections;
using System.Collections.Generic;
using Code.Configs;
using Code.Configs.Rewards;
using Code.Configs.Settings;
using Code.Enums;
using Code.Models;
using Code.Repositories;
using Code.Repositories.Models;
using Code.UnityUtils;
using Code.Views.UI;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Object = UnityEngine.Object;

namespace Code.Controllers.Start
{
    public sealed class DailyRewardsController: BaseController
    {
        private const string NotificationName = "Ежедневные Награды";
        private const string LocalizationTable = "Rewards";
        private const string LocalizationTimerText = "timer_text";
        private const string LocalizationDayText = "day_text";
        
        private readonly DailyRewardsView _dailyRewardsView;
        private readonly UnityLocalizationTools _unityLocalizationTools;
        private readonly SettingsRewardConfig _settingsRewardConfig;
        private readonly RewardConfig[] _rewardConfigs;

        private readonly SavesRepository _savesRepository;
        private readonly RewardSaveModel _rewardSaveModel;
        
        private readonly PlayerProfileModel _playerProfileModel;
        
        private List<SlotRewardView> _slots;
        private bool _isNotificationSended;
        private bool _isGetReward;
        
        public DailyRewardsController(DailyRewardsView dailyRewardsView, PlayerProfileModel playerProfileModel, DataSources dataSources)
        {
            _playerProfileModel = playerProfileModel;
            _settingsRewardConfig = dataSources.SettingsDataSource.SettingsRewardConfig;
            _rewardConfigs = dataSources.RewardsDataSource.RewardConfigs;
            _dailyRewardsView = dailyRewardsView;
            
            _slots = new List<SlotRewardView>();
            
            _savesRepository = _playerProfileModel.SavesRepository;
            _rewardSaveModel = _savesRepository.RewardSaveModel;
            AddController(_savesRepository);

            _unityLocalizationTools = new UnityLocalizationTools(LocalizationTable);
            AddController(_unityLocalizationTools);

            InitSlots();
            _dailyRewardsView.Init(CloseMenu, ClaimReward);
        }

        public void OpenMenu()
        {
            _dailyRewardsView.transform.localScale = Vector3.zero;
            
            _dailyRewardsView.gameObject.SetActive(true);
            _dailyRewardsView.StartCoroutine(RewardsStartUpdate());
            _dailyRewardsView.transform.DOScale(1f, 0.5f);

        }

        private void CloseMenu()
        {
            _dailyRewardsView.transform.DOScale(0f, 0.5f).OnComplete(() =>
            {
                _dailyRewardsView.gameObject.SetActive(false);
                _dailyRewardsView.StopCoroutine(RewardsStartUpdate());
            });
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            var reward = _rewardConfigs[_savesRepository.RewardSaveModel.CurrentRewardSlot];

            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    _savesRepository.CurrencySaveModel.CurrencyWoodCount += reward.CountCurrency;
                    break;
                case RewardType.Metal:
                    _savesRepository.CurrencySaveModel.CurrencyMetalCount += reward.CountCurrency;
                    break;
                case RewardType.Money:
                    _savesRepository.CurrencySaveModel.CurrencyMoneyCount += reward.CountCurrency;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reward.RewardType), reward.RewardType, null);
            }

            _rewardSaveModel.CurrentRewardSlot = (_savesRepository.RewardSaveModel.CurrentRewardSlot + 1) % _rewardConfigs.Length;
            _rewardSaveModel.TimeToReward = DateTime.UtcNow;
            
            RefreshRewardsState();
        }

        // TODO: Лучше сделать через Unirx
        private IEnumerator RewardsStartUpdate()
        {
            while (true)
            {
                RefreshRewardsState();
                yield return new WaitForSeconds(1);
            }
        }

        private void RefreshRewardsState()
        {
            _isGetReward = true;
            if (_rewardSaveModel.TimeToReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _rewardSaveModel.TimeToReward.Value;
                if (timeSpan.TotalSeconds > _settingsRewardConfig.TimeDeadLine)
                {
                    _playerProfileModel.UnityNotificationTools.CreateNotification(
                        NotificationName, "Вы пропустили ежедневную награду!", Importance.Low
                    );
                    _rewardSaveModel.Delete();
                }
                else if (timeSpan.TotalSeconds < _settingsRewardConfig.TimeDeadLine)
                {
                    _isGetReward = false;
                    _isNotificationSended = false;
                }
            }

            SendNotification();
            RefreshUi();
        }

        private void SendNotification()
        {
            if (_isGetReward && !_isNotificationSended)
            {
                _playerProfileModel.UnityNotificationTools.CreateNotification(
                    NotificationName, "Вам доступна ежедневная награда!", Importance.High
                );
                _isNotificationSended = true;
            }
        }

        private void RefreshUi()
        {
            _dailyRewardsView.SetInteractableButton(_isGetReward);
            if (_isGetReward)
            {
                _dailyRewardsView.TimerNewReward.text = "Награда Доступна!";
            }
            else
            {
                if (_rewardSaveModel.TimeToReward.HasValue)
                {
                    var nextClaimTime = _rewardSaveModel.TimeToReward.Value.AddSeconds(_settingsRewardConfig.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward = _unityLocalizationTools.GetLocalizedString(LocalizationTimerText, $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Seconds:D2}");
                    _dailyRewardsView.TimerNewReward.text = timeGetReward;
                    _dailyRewardsView.TimerSlider.value = (float) (currentClaimCooldown.TotalSeconds / _settingsRewardConfig.TimeCooldown);
                }
            }

            for (var i = 0; i < _slots.Count; i++)
            {
                var dayCount = _unityLocalizationTools.GetLocalizedString(LocalizationDayText, i + 1);
                _slots[i].SetData(_rewardConfigs[i], dayCount, i == _rewardSaveModel.CurrentRewardSlot);
            }
        }

        private void InitSlots()
        {
            for (var i = 0; i < _rewardConfigs.Length; i++)
            {
                var instanceSlot = Object.Instantiate(_dailyRewardsView.SlotRewardViewPrefab, _dailyRewardsView.GridSlotsReward, false);
                
                _slots.Add(instanceSlot);
            }
        }
    }
}