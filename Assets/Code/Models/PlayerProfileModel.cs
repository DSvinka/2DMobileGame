using Code.Analytics;
using Code.Configs;
using Code.Configs.Settings;
using Code.Enums;
using Code.Properties;
using Code.Repositories;
using UnityEngine;

namespace Code.Models
{
    public sealed class PlayerProfileModel
    {
        #region Приватные Поля

        private float _speed;

        private CarModel _currentCarModel;
        private SavesRepository _savesRepository;
        private UpgradesRepository _upgradesRepository;
        private SettingsPlayerConfig _settingsPlayerConfig;

        private SubscribeProperty<GameState> _currentGameState;

        private IUnityPurchasingTools _unityPurchasingTools;
        private IAnalyticsTools _analyticsTools;
        private IAdsShower _adsShower;

        #endregion

        #region Публичные Свойства

        public float Speed => _speed;
        public CarModel CurrentCarModel => _currentCarModel;
        public SavesRepository SavesRepository => _savesRepository;
        public UpgradesRepository UpgradesRepository => _upgradesRepository;

        public SubscribeProperty<GameState> CurrentGameState => _currentGameState;

        public IUnityPurchasingTools UnityPurchasingTools => _unityPurchasingTools;
        public IAnalyticsTools AnalyticsTools => _analyticsTools;
        public IAdsShower AdsShower => _adsShower;

        #endregion

        public PlayerProfileModel(float speed, DataSources dataSources, IAdsShower unityAdsTools, IUnityPurchasingTools unityPurchasingTools)
        {
            _speed = speed;
            _savesRepository = new SavesRepository();
            _upgradesRepository = new UpgradesRepository(dataSources.UpgradesDataSource);
            _settingsPlayerConfig = dataSources.SettingsDataSource.SettingsPlayerConfig;
            _currentGameState = new SubscribeProperty<GameState>();

            _analyticsTools = new UnityAnalyticsTools();
            _unityPurchasingTools = unityPurchasingTools;
            _adsShower = unityAdsTools;
            
            Reset();
        }

        public void SetupUpgrades()
        {
            var upgradesCollection = _upgradesRepository.Collection;
            var upgradesSave = _savesRepository.UpgradesSaveModel;

            if (upgradesSave.ArmorUpgradeID > 0)
                _currentCarModel.SetUpgrade(upgradesCollection[upgradesSave.ArmorUpgradeID]);
            
            if (upgradesSave.BulletUpgradeID > 0)
                _currentCarModel.SetUpgrade(upgradesCollection[upgradesSave.BulletUpgradeID]);
            
            if (upgradesSave.BulletPowderUpgradeID > 0)
                _currentCarModel.SetUpgrade(upgradesCollection[upgradesSave.BulletPowderUpgradeID]);
            
            if (upgradesSave.GunUpgradeID > 0)
                _currentCarModel.SetUpgrade(upgradesCollection[upgradesSave.GunUpgradeID]);
        }

        public void Reset()
        {
            _currentCarModel = new CarModel(new CarModelConfig()
            {
                EntityView = null,
                
                MaxHealth = _settingsPlayerConfig.Health,
                ShotRate = _settingsPlayerConfig.BulletShotRate,
                
                BulletLifeTime = _settingsPlayerConfig.BulletLifeTime,
                BulletShotForce = _settingsPlayerConfig.BulletShotForce,
                BulletDamage = _settingsPlayerConfig.Damage,
            });
        }
    }
}