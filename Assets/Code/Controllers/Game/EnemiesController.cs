using System;
using System.Collections.Generic;
using Code.Configs.Enemies;
using Code.Enums;
using Code.Models;
using Code.Properties;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    // TODO: Возможно нужно объяденить класс EnemiesController и класс TurretController, а конкретней функционал турелей.
    public sealed class EnemiesController : BaseController
    {
        private readonly ResourcePath _bulletViewPath = new ResourcePath() { PathResource = "Prefabs/BulletView" };

        private readonly EnemiesDataSource _enemiesDataSource;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SubscribeProperty<float> _moveUpdate;
        private readonly Dictionary<int, EnemyModel> _enemies;

        private Transform _enemiesPoolTransform;
        private Transform _bulletsPoolTransform;

        public EnemiesController(InputModel inputModel, PlayerProfileModel playerProfileModel, EnemiesDataSource enemiesDataSource)
        {
            _playerProfileModel = playerProfileModel;
            _enemiesDataSource = enemiesDataSource;
            _moveUpdate = inputModel.MoveUpdate;

            _enemies = new Dictionary<int, EnemyModel>();
            
            SetupEnemies();
            SetupPools();
            
            _moveUpdate.SubscribeOnChange(Execute);
        }

        protected override void OnDispose()
        {
            _moveUpdate.UnSubscribeOnChange(Execute);

            foreach (var enemy in _enemies)
            {
                DestroyEnemy(enemy.Key);
            }
            _enemies.Clear();
        }

        // TODO: Нужно сделать нормальный пул объектов для пуль.
        private void SetupPools()
        {
            var enemiesPool = new GameObject("Enemies Pool");
            _enemiesPoolTransform = enemiesPool.transform;
            AddGameObject(enemiesPool);
            
            var bulletsPool = new GameObject("EnemyBullets Pool");
            _bulletsPoolTransform = bulletsPool.transform;
            AddGameObject(bulletsPool);
        }

        private void SetupEnemies()
        {
            foreach (var enemyConfig in _enemiesDataSource.EnemyConfigs)
            {
                var carModel = _playerProfileModel.CurrentCarModel;
                var enemy = CreateEnemy(enemyConfig.EnemyView);
                enemy.Init(EntityType.Enemy);
                enemy.OnDamage += OnEnemyDamage;

                var enemyModel = new EnemyModel(enemyConfig, enemy);
                enemyModel.OnDeath += OnEnemyDeath;
                enemyModel.SetPlayerStats(carModel.Health, carModel.BulletDamage);
                
                enemy.UpdateHealthDisplay(enemyModel.Health);
                _enemies.Add(enemy.gameObject.GetInstanceID(), enemyModel);
            }
        }

        private EnemyView CreateEnemy(EnemyView prefab)
        {
            return Object.Instantiate(prefab, _enemiesPoolTransform, true);
        }
        
        private void DestroyEnemy(int id, bool removeFromDict=false)
        {
            var entityModel = _enemies[id];
            DestroyEnemy(entityModel, removeFromDict);
        }
        
        private void DestroyEnemy(EnemyModel enemyModel, bool removeFromDict=false)
        {
            enemyModel.OnDeath -= OnEnemyDeath;
            enemyModel.EntityView.OnDamage -= OnEnemyDamage;

            Object.Destroy(enemyModel.GameObject);
            
            if (removeFromDict)
                _enemies.Remove(enemyModel.ID);
        }

        private void OnEnemyDeath(int id)
        {
            var entityModel = _enemies[id];
            
            _playerProfileModel.SavesRepository.CurrencySaveModel.CurrencyMoneyCount += entityModel.Config.GiveMoneyOnDeath;
            DestroyEnemy(entityModel, true);
        }

        private void OnEnemyDamage(int id, float damage)
        {
            var entityModel = _enemies[id];
            
            entityModel.AddDamage(damage);
            entityModel.EntityView.UpdateHealthDisplay(entityModel.Health);
        }

        private void Execute(float deltatime)
        {
            foreach (var enemy in _enemies)
            {
                var enemyModel = enemy.Value;
                RotateWheels(enemyModel);
                EnemyMoveTurret(enemyModel);
                EnemyShot(enemyModel, deltatime);
            }
        }

        private void RotateWheels(EnemyModel enemyModel)
        {
            enemyModel.EntityView.RotateWheels(new Vector3(0f, 0f, -_playerProfileModel.Speed));
        }

        private void EnemyMoveTurret(EnemyModel enemyModel)
        {
            var turretView = enemyModel.EntityView.TurretView;
            var playerTransform = _playerProfileModel.CurrentCarModel.EntityView.TurretView.Gun.transform;
                
            var diff = playerTransform.position - turretView.Gun.position;
            diff.Normalize();
            
            enemyModel.RotationToPlayerZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            turretView.Gun.rotation = Quaternion.Euler(0f, 0f, enemyModel.RotationToPlayerZ + 90);
        }
        
        // TODO: Переделать с постоянного удаления/создания пуль на паттерн Pool Объектов!
        private void EnemyShot(EnemyModel enemyModel, float deltatime)
        {
            var turretView = enemyModel.EntityView.TurretView;
                
            enemyModel.ReduceCooldown(deltatime);
            if (!enemyModel.CheckCooldown()) 
                return;
            enemyModel.ResetCooldown();
                
            
            var bulletView = CreateBullet();
            bulletView.Init(enemyModel.BulletDamage, EntityType.Enemy);
            
            var transform = bulletView.transform;
            transform.parent = _bulletsPoolTransform;
            transform.position = turretView.ShotPoint.position;
            transform.rotation = Quaternion.Euler(0f, 0f, enemyModel.RotationToPlayerZ);
        
            bulletView.Rigidbody.AddForce(transform.right * enemyModel.Config.BulletShotForce, ForceMode2D.Impulse);
        
            Object.Destroy(bulletView.gameObject, enemyModel.Config.BulletLifeTime);
        }

        private BulletView CreateBullet()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_bulletViewPath));
            if (!objectView.TryGetComponent<BulletView>(out var bulletView))
                throw new Exception("Компонент BulletView не найден на View объекте!");

            return bulletView;
        }
    }
}