using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Enums;
using Code.Interfaces;
using Code.Interfaces.Models;
using Code.Models;
using Code.Repositories;
using Code.Views.Garage;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Garage
{
    public sealed class UpgradesController: BaseController
    {
        private readonly UpgradesView _upgradesView;
        private readonly SavesRepository _savesRepository;
        private readonly IRepository<int, IUpgradeModel> _upgradesRepository;
        private readonly PlayerProfileModel _playerProfileModel;

        private readonly Dictionary<int, SlotUpgradeModel> _upgrades;
        private readonly Dictionary<UpgradeType, SlotUpgradeView> _disabledUpgraders;

        public UpgradesController(GarageView garageView, PlayerProfileModel playerProfileModel, DataSources dataSources)
        {
            playerProfileModel.SetupUpgrades();
            
            _upgradesView = garageView.UpgradeMenuView;
            var upgradesRepository = new UpgradesRepository(dataSources.UpgradesDataSource);
            AddController(upgradesRepository);

            _playerProfileModel = playerProfileModel;
            _savesRepository = playerProfileModel.SavesRepository;
            _upgradesRepository = upgradesRepository;
            
            _upgrades = new Dictionary<int, SlotUpgradeModel>();
            _disabledUpgraders = new Dictionary<UpgradeType, SlotUpgradeView>();
        }

        public void Load()
        {
            foreach (var upgrade in _upgradesRepository.Collection)
            {
                var value = upgrade.Value;

                var itemView = LoadItemView(_upgradesView.GridSlotsUpgrades);
                itemView.OnClick += BuyUpgrade;
                itemView.Init(value.Info);
                
                if (CheckPlayerHaveUpgrade(value))
                {
                    itemView.SetActiveButton(false);
                    _disabledUpgraders.Add(value.UpgradeType, itemView);
                }

                var slotUpgradeModel = new SlotUpgradeModel(itemView, value);
                _upgrades.Add(itemView.gameObject.GetInstanceID(), slotUpgradeModel);
            }
        }

        private bool CheckPlayerHaveUpgrade(IUpgradeModel upgradeModel)
        {
            var carModel = _playerProfileModel.CurrentCarModel;
            if (carModel.Upgrades.TryGetValue(upgradeModel.UpgradeType, out var savedUpgradeModel))
            {
                return savedUpgradeModel.ID == upgradeModel.ID;
            }

            return false;
        }

        private void BuyUpgrade(SlotUpgradeView slotUpgradeView, int id)
        {
            var upgrade = _upgrades[id];
            var upgradeModel = upgrade.UpgradeModel;
            var currencySaveModel = _savesRepository.CurrencySaveModel;

            switch (upgradeModel.PriceCurrency)
            {
                case CurrencyType.Metal:
                    if (currencySaveModel.CurrencyMetalCount < upgradeModel.Price)
                        return;
                        
                    currencySaveModel.CurrencyMetalCount -= upgradeModel.Price;
                    break;
                case CurrencyType.Money:
                    if (currencySaveModel.CurrencyMoneyCount < upgradeModel.Price)
                        return;
                    
                    currencySaveModel.CurrencyMoneyCount -= upgradeModel.Price;
                    break;
                case CurrencyType.Wood:
                    if (currencySaveModel.CurrencyWoodCount < upgradeModel.Price)
                        return;
                    
                    currencySaveModel.CurrencyWoodCount -= upgradeModel.Price;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradeModel.PriceCurrency), upgradeModel.PriceCurrency, null);
            }
            
            switch (upgradeModel.UpgradeType)
            {
                case UpgradeType.Health:
                    _savesRepository.UpgradesSaveModel.ArmorUpgradeID = upgradeModel.ID;
                    break;
                case UpgradeType.Damage:
                    _savesRepository.UpgradesSaveModel.BulletUpgradeID = upgradeModel.ID;
                    break;
                case UpgradeType.ShotRate:
                    _savesRepository.UpgradesSaveModel.GunUpgradeID = upgradeModel.ID;
                    break;
                case UpgradeType.ShotForce:
                    _savesRepository.UpgradesSaveModel.BulletPowderUpgradeID = upgradeModel.ID;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradeModel.UpgradeType), upgradeModel.UpgradeType, null);
            }

            if (_disabledUpgraders.TryGetValue(upgradeModel.UpgradeType, out var oldUpgradeView))
            {
                oldUpgradeView.SetActiveButton(true);
                _disabledUpgraders.Remove(upgradeModel.UpgradeType);
            }
            
            slotUpgradeView.SetActiveButton(false);
            _disabledUpgraders.Add(upgradeModel.UpgradeType, slotUpgradeView);

            Debug.Log($"{upgradeModel.UpgradeType} - {upgradeModel.ValueUpgrade}");
        }
        
        private SlotUpgradeView LoadItemView(Transform spawnPosition)
        {
            var slotUpgradeView = Object.Instantiate(_upgradesView.SlotUpgradeViewPrefab, spawnPosition, false);
            AddGameObject(slotUpgradeView.gameObject);

            return slotUpgradeView;
        }

        protected override void OnDispose()
        {
            foreach (var item in _upgrades)
            {
                item.Value.SlotUpgradeView.OnClick -= BuyUpgrade;
            }
            _upgrades.Clear();
            _disabledUpgraders.Clear();
        }
    }
}