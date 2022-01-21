using System;
using Code.Enums;
using Code.Interfaces.Models;
using Code.Views;
using UnityEditor;
using UnityEngine;

namespace Code.Models
{
    public sealed class EnemyModel: IEntityModel<EnemyView>
    {
        #region Поля
        
        private EnemyView _enemyView;
        private GameObject _gameObject;
        private Transform _transform;
        
        private int _gameObjectID;

        private readonly float _maxHealth;
        private readonly float _shotRate;

        private readonly float _bulletLifeTime;
        private readonly float _bulletShotForce;
        private readonly float _bulletDamage;

        private float _health;
        private float _shotCooldown;
        private float _rotationToPlayerZ;
        
        private float _playerHealth;
        private float _playerBulletDamage;

        #endregion

        #region Свойства

        public EnemyView EntityView => _enemyView;
        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;

        public int ID => _gameObjectID;

        public float MaxHealth => _maxHealth;
        public float ShotRate => _shotRate;

        public float BulletLifeTime => _bulletLifeTime;
        public float BulletShotForce => _bulletShotForce;
        public float BulletDamage
        {
            get
            {
                var damage = (_bulletDamage + _playerBulletDamage) / 2;
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

        public EnemyModel(EnemyModelConfig config)
        {
            SetEntityView(config.EntityView);
            _playerHealth = config.PlayerCarModel.Health;
            _playerBulletDamage = config.PlayerCarModel.BulletDamage;
            
            _maxHealth = config.MaxHealth;
            _shotRate = config.ShotRate;

            _bulletLifeTime = config.BulletLifeTime;
            _bulletShotForce = config.BulletShotForce;
            _bulletDamage = config.BulletDamage;

            _health = _maxHealth;
            _shotCooldown = _shotRate;
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
        
        public bool CheckCooldown()
        {
            return _shotCooldown <= 0;
        }
        public void ResetCooldown()
        {
            _shotCooldown = _shotRate;
        }
        public void ReduceCooldown(float count)
        {
            _shotCooldown -= count;
        }
    }

    public struct EnemyModelConfig: IEntityModelConfig<EnemyView>
    {
        private EnemyView _enemyView;
        private CarModel _playerCarModel;
        
        private float _maxHealth;
        private float _shotRate;
        
        private float _bulletLifeTime;
        private float _bulletShotForce;
        private float _bulletDamage;

        public EnemyView EntityView
        {
            get => _enemyView;
            set => _enemyView = value;
        }
        public CarModel PlayerCarModel
        {
            get => _playerCarModel;
            set => _playerCarModel = value;
        }

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }
        public float ShotRate
        {
            get => _shotRate;
            set => _shotRate = value;
        }

        public float BulletLifeTime
        {
            get => _bulletLifeTime;
            set => _bulletLifeTime = value;
        }
        public float BulletShotForce
        {
            get => _bulletShotForce;
            set => _bulletShotForce = value;
        }
        public float BulletDamage
        {
            get => _bulletDamage;
            set => _bulletDamage = value;
        }
    }
}