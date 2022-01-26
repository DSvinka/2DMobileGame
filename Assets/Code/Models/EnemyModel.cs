using System;
using Code.Configs.Enemies;
using Code.Enums;
using Code.Interfaces.Models;
using Code.Views;
using UnityEditor;
using UnityEngine;

namespace Code.Models
{
    public sealed class EnemyModel
    {
        #region Поля

        private EnemyConfig _enemyConfig;
        
        private int _gameObjectID;
        private EnemyView _enemyView;
        private GameObject _gameObject;
        private Transform _transform;

        private float _health;
        private float _shotCooldown;
        private float _rotationToPlayerZ;
        
        private float _playerMaxHealth;
        private float _playerBulletDamage;

        #endregion

        #region Свойства
        
        public EnemyConfig Config => _enemyConfig;
        public int ID => _gameObjectID;
        
        public EnemyView EntityView => _enemyView;
        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;

        public float BulletDamage
        {
            get
            {
                var damage = (_enemyConfig.BulletDamage + _playerBulletDamage) / 2;
                return Mathf.Abs(damage);
            }
        }

        public float Health => _health;
        public float ShotCooldown => _shotCooldown;
        public float RotationToPlayerZ 
        {
            get => _rotationToPlayerZ;
            set => _rotationToPlayerZ = value;
        }

        #endregion
        
        public event Action<int> OnDeath;
        public event Action<int, float> OnDamage;

        public EnemyModel(EnemyConfig config, EnemyView enemyView)
        {
            SetEntityView(enemyView);
            _enemyConfig = config;

            _health = config.MaxHealth;
            _shotCooldown = config.ShotRate;
        }

        public void SetEntityView(EnemyView entityView)
        {
            _enemyView = entityView;
            _transform = entityView.transform;
            _gameObject = entityView.gameObject;
            
            _gameObjectID = _gameObject.GetInstanceID();
        }

        public void AddDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
                OnDeath?.Invoke(_gameObjectID);
            OnDamage?.Invoke(_gameObjectID, damage);
        }

        public void SetPlayerStats(float playerMaxHealth, float playerBulletDamage)
        {
            _playerMaxHealth = playerMaxHealth;
            _playerBulletDamage = playerBulletDamage;
        }
        
        public bool CheckCooldown()
        {
            return _shotCooldown <= 0;
        }
        public void ResetCooldown()
        {
            _shotCooldown = _enemyConfig.ShotRate;
        }
        public void ReduceCooldown(float count)
        {
            _shotCooldown -= count;
        }
    }
}