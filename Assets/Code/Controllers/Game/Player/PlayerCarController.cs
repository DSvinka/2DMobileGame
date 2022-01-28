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
    public sealed class PlayerCarController: BaseController
    {
        private readonly ResourcePath _viewPath;
        private readonly PlayerCarView _playerCarView;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SubscribeProperty<float> _moveUpdate;

        public PlayerCarController(InputModel inputModel, PlayerProfileModel playerProfileModel, Camera camera)
        {
            _playerProfileModel = playerProfileModel;
            _moveUpdate = inputModel.MoveUpdate;
            _moveUpdate.SubscribeOnChange(RotateWheels);
            
            _viewPath = _playerProfileModel.CurrentCarModel.ResourcePath;
            _playerCarView = LoadView();
            _playerCarView.Init(EntityType.Player);
            _playerCarView.OnDamage += OnDamage;

            _playerProfileModel.CurrentCarModel.SetEntityView(_playerCarView);
            _playerProfileModel.SetupUpgrades();

            _playerCarView.UpdateHealthDisplay(_playerProfileModel.CurrentCarModel.Health);

            var turretController = new PlayerTurretController(inputModel, camera, _playerProfileModel);
            AddController(turretController);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _moveUpdate.UnSubscribeOnChange(RotateWheels);
            _playerCarView.OnDamage -= OnDamage;
        }

        private void OnDamage(int id, float damage)
        {
            var carModel = _playerProfileModel.CurrentCarModel;
            carModel.AddDamage(damage);
            _playerCarView.UpdateHealthDisplay(carModel.Health);
        }

        private PlayerCarView LoadView()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<PlayerCarView>(out var carView))
                throw new Exception("Компонент CarView не найден на View объекте!");

            return carView;
        }

        private void RotateWheels(float deltatime)
        {
            _playerCarView.RotateWheels(new Vector3(0f, 0f, -_playerProfileModel.Speed));
        }
    }
}