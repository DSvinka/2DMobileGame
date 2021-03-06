using System;
using System.Collections.Generic;
using Code.Enums;
using Code.Interfaces.Models;
using Code.Utils;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    public sealed class CarModel: IEntityModel<PlayerCarView>
    {
        public ResourcePath ResourcePath = new ResourcePath() { PathResource = "Prefabs/Cars/Car" };

        #region Поля

        private Dictionary<UpgradeType, IUpgradeModel> _upgrades;

        private PlayerCarView _playerCarView;
        private GameObject _gameObject;
        private Transform _transform;
        
        private int _gameObjectID;
        
        private float _maxHealth;
        private float _shotRate;
        
        private float _bulletLifeTime;
        private float _bulletShotForce;
        private float _bulletDamage;
        
        private float _health;
        private float _shotCooldown;

        #endregion

        #region Свойства
        
        public IReadOnlyDictionary<UpgradeType, IUpgradeModel> Upgrades => _upgrades;

        public PlayerCarView EntityView => _playerCarView;
        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        
        public int ID => _gameObjectID;

        public float MaxHealth => _maxHealth;
        public float ShotRate => _shotRate;

        public float BulletLifeTime => _bulletLifeTime;
        public float BulletShotForce => _bulletShotForce;
        public float BulletDamage => _bulletDamage;
        
        public float Health => _health;
        public float ShotCooldown => _shotCooldown;

        #endregion

        public event Action<int> OnDeath;
        public event Action<int, float> OnDamage;

        public CarModel(CarModelConfig config)
        {
            _upgrades = new Dictionary<UpgradeType, IUpgradeModel>(4);
            if (config.EntityView != null)
                SetEntityView(config.EntityView);

            _maxHealth = config.MaxHealth;
            _shotRate = config.ShotRate;

            _bulletLifeTime = config.BulletLifeTime;
            _bulletShotForce = config.BulletShotForce;
            _bulletDamage = config.BulletDamage;

            _health = _maxHealth;
            _shotCooldown = _shotRate;
        }

        public void SetEntityView(PlayerCarView playerCarView)
        {
            _playerCarView = playerCarView;
            _transform = _playerCarView.transform;
            _gameObject = _playerCarView.gameObject;
            
            _gameObjectID = _gameObject.GetInstanceID();
        }
        
        public void AddDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
                OnDeath?.Invoke(_gameObjectID);
            OnDamage?.Invoke(_gameObjectID, damage);
        }

        public bool CheckCooldown()
        {
            return _shotCooldown <= 0;
        }
        public void ReduceCooldown(float count)
        {
            _shotCooldown -= count;
        }
        public void ResetCooldown()
        {
            _shotCooldown = _shotRate;
        }

        public void SetUpgrade(IUpgradeModel upgrade)
        {
            if (_upgrades.ContainsKey(upgrade.UpgradeType))
                RemoveUpgrade(upgrade);
            
            switch(upgrade.UpgradeType)
            {
                case UpgradeType.Health:
                    _health += upgrade.ValueUpgrade;
                    break;
                case UpgradeType.Damage:
                    _bulletDamage += upgrade.ValueUpgrade;
                    break;
                case UpgradeType.ShotRate:
                    _shotRate += upgrade.ValueUpgrade;
                    break;
                case UpgradeType.ShotForce:
                    _bulletShotForce += upgrade.ValueUpgrade;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _upgrades.Add(upgrade.UpgradeType, upgrade);
        }
        
        public void RemoveUpgrade(IUpgradeModel upgrade)
        {
            if (!_upgrades.ContainsKey(upgrade.UpgradeType))
                return;
                    
            switch(upgrade.UpgradeType)
            {
                case UpgradeType.Health:
                    _health -= upgrade.ValueUpgrade;
                    break;
                case UpgradeType.Damage:
                    _bulletDamage -= upgrade.ValueUpgrade;
                    break;
                case UpgradeType.ShotRate:
                    _shotRate -= upgrade.ValueUpgrade;
                    break;
                case UpgradeType.ShotForce:
                    _bulletShotForce -= upgrade.ValueUpgrade;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _upgrades.Remove(upgrade.UpgradeType);
        }
    }
    
    public struct CarModelConfig: IEntityModelConfig<PlayerCarView>
    {
        public PlayerCarView EntityView { get; set; }
        
        public float MaxHealth { get; set; }
        public float ShotRate { get; set; }
        
        public float BulletLifeTime { get; set; }
        public float BulletShotForce { get; set; }
        public float BulletDamage { get; set; }
    }
}