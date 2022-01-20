using System;
using System.Collections.Generic;
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
        private readonly ResourcePath _enemyViewPath = new ResourcePath() {PathResource = "Prefabs/Cars/CarRed"};
        private readonly ResourcePath _bulletViewPath = new ResourcePath() { PathResource = "Prefabs/BulletView" };

        private PlayerProfileModel _playerProfileModel;
        private SubscribeProperty<float> _moveUpdate;
        private Dictionary<int, EnemyModel> _enemies;

        private Transform _enemiesPoolTransform;
        private Transform _bulletsPoolTransform;

        public EnemiesController(InputModel inputModel, PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
            _moveUpdate = inputModel.MoveUpdate;

            var enemy = CreateEnemy();
            enemy.Init(EntityType.Enemy);
            var enemyConfig = new EnemyModelConfig()
            {
                EntityView = enemy,
                PlayerCarModel = _playerProfileModel.CurrentCarModel,
                MaxHealth = 100f,
                ShotRate = 2f,
                BulletLifeTime = 20f,
                BulletShotForce = 15f,
                BulletDamage = 20f,
            };
            var enemyModel = new EnemyModel(enemyConfig);

            _enemies = new Dictionary<int, EnemyModel>()
            {
                {enemy.gameObject.GetInstanceID(), enemyModel}
            };
            
            enemy.OnDamage += OnEnemyDamage;
            enemyModel.OnDeath += OnEnemyDeath;
            _moveUpdate.SubscribeOnChange(MoveEnemyTurrets);
            _moveUpdate.SubscribeOnChange(EnemiesShot);
            
            var enemiesPool = new GameObject("Enemies Pool");
            _enemiesPoolTransform = enemiesPool.transform;
            AddGameObject(enemiesPool);
            
            var bulletsPool = new GameObject("EnemyBullets Pool");
            _bulletsPoolTransform = bulletsPool.transform;
            AddGameObject(bulletsPool);
        }

        private EnemyView CreateEnemy()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_enemyViewPath), _enemiesPoolTransform, true);

            if (!objView.TryGetComponent<EnemyView>(out var enemyView))
                throw new Exception("Компонент EnemyView не найден на View объекте!");
            
            return enemyView;
        }

        private void DestroyEnemy(int id, bool removeFromDict=false)
        {
            var enemy = _enemies[id];
            enemy.OnDeath -= OnEnemyDeath;
            enemy.EntityView.OnDamage -= OnEnemyDamage;

            Object.Destroy(_enemies[id].GameObject);
            
            if (removeFromDict)
                _enemies.Remove(id);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _moveUpdate.UnSubscribeOnChange(MoveEnemyTurrets);
            foreach (var enemy in _enemies)
            {
                DestroyEnemy(enemy.Key, false);
            }
        }

        private void OnEnemyDeath(int id)
        {
            DestroyEnemy(id, true);
        }

        private void OnEnemyDamage(int id, float damage)
        {
            var entityModel = _enemies[id];
            
            entityModel.AddDamage(damage);
            entityModel.EntityView.HealthText.text = $"HP: {entityModel.Health}";
        }

        private void MoveEnemyTurrets(float deltatime)
        {
            foreach (var enemy in _enemies)
            {
                var enemyModel = enemy.Value;
                var turretView = enemyModel.EntityView.TurretView;
                var playerTransform = _playerProfileModel.CurrentCarModel.EntityView.TurretView.Gun.transform;
                
                var diff = playerTransform.position - turretView.Gun.position;
                diff.Normalize();
            
                enemyModel.RotationToPlayerZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                turretView.Gun.rotation = Quaternion.Euler(0f, 0f, enemyModel.RotationToPlayerZ + 90);
            }
        }
        
        private BulletView CreateBullet()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_bulletViewPath));
            if (!objectView.TryGetComponent<BulletView>(out var bulletView))
                throw new Exception("Компонент BulletView не найден на View объекте!");

            return bulletView;
        }

        // TODO: Переделать с постоянного удаления/создания пуль на паттерн Pool Объектов!
        private void EnemiesShot(float deltatime)
        {
            foreach (var enemy in _enemies)
            {
                var enemyModel = enemy.Value;
                var turretView = enemyModel.EntityView.TurretView;
                
                enemyModel.ReduceCooldown(deltatime);
                if (!enemyModel.CheckCooldown()) 
                    continue;
                enemyModel.ResetCooldown();
                    
                
                var bulletView = CreateBullet();
                bulletView.Init(enemyModel.BulletDamage, EntityType.Enemy);
                
                var transform = bulletView.transform;
                transform.parent = _bulletsPoolTransform;
                transform.position = turretView.ShotPoint.position;
                transform.rotation = Quaternion.Euler(0f, 0f, enemyModel.RotationToPlayerZ);
            
                bulletView.Rigidbody.AddForce(transform.right * enemyModel.BulletShotForce, ForceMode2D.Impulse);
            
                Object.Destroy(bulletView.gameObject, enemyModel.BulletLifeTime);
            }
        }
    }
}