using System;
using System.Collections;
using System.Collections.Generic;
using Code.Configs;
using Code.Configs.Rewards;
using Code.Configs.Settings;
using Code.Models;
using Code.Repositories;
using Code.Types;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Start
{
    public sealed class DailyRewardsController: BaseController
    {
        private readonly DailyRewardsView _dailyRewardsView;
        private readonly SettingsRewardConfig _settingsRewardConfig;
        private readonly RewardConfig[] _rewardConfigs;

        private readonly SavesRepository _savesRepository;
        private readonly SaveRewardModel _saveRewardModel;
        
        private readonly PlayerProfileModel _playerProfileModel;
        
        private List<SlotRewardView> _slots;
        private bool _isGetReward;
        
        public DailyRewardsController(DailyRewardsView dailyRewardsView, PlayerProfileModel playerProfileModel, DataSources dataSources)
        {
            _playerProfileModel = playerProfileModel;
            _settingsRewardConfig = dataSources.SettingsDataSource.SettingsRewardConfig;
            _rewardConfigs = dataSources.RewardsDataSource.RewardConfigs;
            _dailyRewardsView = dailyRewardsView;
            
            _slots = new List<SlotRewardView>();
            
            _savesRepository = _playerProfileModel.SavesRepository;
            _saveRewardModel = _savesRepository.SaveRewardModel;
            AddController(_savesRepository);

            InitSlots();
            _dailyRewardsView.Init(CloseMenu, ClaimReward);
        }

        public void OpenMenu()
        {
            _dailyRewardsView.gameObject.SetActive(true);
            _dailyRewardsView.StartCoroutine(RewardsStartUpdate());
        }

        public void CloseMenu()
        {
            _dailyRewardsView.gameObject.SetActive(false);
            _dailyRewardsView.StopCoroutine(RewardsStartUpdate());
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            var reward = _rewardConfigs[_savesRepository.SaveRewardModel.CurrentRewardSlot];

            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    _savesRepository.SaveCurrencyModel.CurrencyWoodCount = reward.CountCurrency;
                    break;
                case RewardType.Metal:
                    _savesRepository.SaveCurrencyModel.CurrencyMetalCount = reward.CountCurrency;
                    break;
                case RewardType.Money:
                    _savesRepository.SaveCurrencyModel.CurrencyMoneyCount = reward.CountCurrency;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reward.RewardType), reward.RewardType, null);
            }

            _saveRewardModel.CurrentRewardSlot = (_savesRepository.SaveRewardModel.CurrentRewardSlot + 1) % _rewardConfigs.Length;
            _saveRewardModel.TimeToReward = DateTime.UtcNow;
            
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
            if (_saveRewardModel.TimeToReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _saveRewardModel.TimeToReward.Value;
                if (timeSpan.TotalSeconds > _settingsRewardConfig.TimeDeadLine)
                {
                    _saveRewardModel.Delete();
                }
                else if (timeSpan.TotalSeconds < _settingsRewardConfig.TimeDeadLine)
                {
                    _isGetReward = false;
                }
            }
            
            RefreshUi();
        }

        private void RefreshUi()
        {
            _dailyRewardsView.TimerSlider.minValue = 0;
            _dailyRewardsView.TimerSlider.maxValue = _settingsRewardConfig.TimeCooldown;
            
            _dailyRewardsView.SetInteractableButton(_isGetReward);
            if (_isGetReward)
            {
                _dailyRewardsView.TimerNewReward.text = "Награда Доступна!";
            }
            else
            {
                if (_saveRewardModel.TimeToReward.HasValue)
                {
                    var nextClaimTime = _saveRewardModel.TimeToReward.Value.AddSeconds(_settingsRewardConfig.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward = $"Награда через {currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Seconds:D2}";
                    _dailyRewardsView.TimerNewReward.text = timeGetReward;
                    _dailyRewardsView.TimerSlider.value = (float) currentClaimCooldown.TotalSeconds;
                }
            }

            for (var i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetData(_rewardConfigs[i], i + 1, i == _saveRewardModel.CurrentRewardSlot);
            }
        }

        private void InitSlots()
        {
            for (var i = 0; i < _rewardConfigs.Length; i++)
            {
                var instanceSlot = Object.Instantiate(_dailyRewardsView.SlotRewardViewPrefab,
                    _dailyRewardsView.GridSlotsReward, false);
                
                _slots.Add(instanceSlot);
            }
        }
    }
}