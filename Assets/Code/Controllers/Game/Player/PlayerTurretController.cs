using System;
using Code.Enums;
using Code.Models;
using Code.Properties;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game.Player
{
    public sealed class PlayerTurretController: BaseController
    {
        private readonly ResourcePath _bulletViewPath = new ResourcePath() { PathResource = "Prefabs/BulletView" };

        private float _rotationToTouchZ;
        private TurretView _turretView;
        private CarModel _carModel;
        private Camera _camera;
        
        private Transform _bulletsPoolTransform;

        private SubscribeProperty<Vector2> _touchPosition;

        public PlayerTurretController(InputModel inputModel, Camera camera, PlayerProfileModel playerProfileModel)
        {
            _touchPosition = inputModel.TouchPosition;
            _touchPosition.SubscribeOnChange(MoveTurret);

            _carModel = playerProfileModel.CurrentCarModel;
            _turretView = _carModel.EntityView.TurretView;
            _turretView.Init(_carModel.ShotRate);
            _turretView.OnShotRequest += Shot;

            var bulletsObject = new GameObject("PlayerBullets Pool");
            _bulletsPoolTransform = bulletsObject.transform;
            AddGameObject(bulletsObject);
            
            _camera = camera;
        }

        protected override void OnDispose()
        {
            _touchPosition.UnSubscribeOnChange(MoveTurret);
            _turretView.OnShotRequest -= Shot;
        }
        
        // TODO: Переделать с постоянного удаления/создания пуль на паттерн Pool Объектов!
        private void Shot(int turretID)
        {
            var bulletView = CreateBullet();
            bulletView.Init(_carModel.BulletDamage, EntityType.Player);
            
            var transform = bulletView.transform;
            transform.parent = _bulletsPoolTransform;
            transform.position = _turretView.ShotPoint.position;
            transform.rotation = Quaternion.Euler(0f, 0f, _rotationToTouchZ);
            
            bulletView.Rigidbody.AddForce(transform.right * _carModel.BulletShotForce, ForceMode2D.Impulse);
            
            Object.Destroy(bulletView.gameObject, _carModel.BulletLifeTime);
        }

        private BulletView CreateBullet()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_bulletViewPath));
            if (!objectView.TryGetComponent<BulletView>(out var bulletView))
                throw new Exception("Компонент BulletView не найден на View объекте!");

            return bulletView;
        }

        private void MoveTurret(Vector2 position)
        {
            var diff = _camera.ScreenToWorldPoint(position) - _turretView.Gun.position;
            diff.Normalize();
            
            _rotationToTouchZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _turretView.Gun.rotation = Quaternion.Euler(0f, 0f, _rotationToTouchZ - 90);
        }
    }
}