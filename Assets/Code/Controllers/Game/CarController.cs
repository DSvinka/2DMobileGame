using System;
using Code.Interfaces.Properties;
using Code.Models;
using Code.Properties;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class CarController: BaseController
    {
        private readonly ResourcePath _viewPath;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly CarView _carView;
        
        private readonly IReadOnlySubscribeProperty<float> _leftMove;
        private readonly IReadOnlySubscribeProperty<float> _rightMove;

        public CarController(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
            
            _viewPath = _playerProfileModel.CurrentCarModel.ResourcePath;
            _carView = LoadView();

            _leftMove = leftMove;
            _rightMove = rightMove;
            
            _leftMove.SubscribeOnChange(RotateWheels);
            _rightMove.SubscribeOnChange(RotateWheels);
        }

        private CarView LoadView()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<CarView>(out var carView))
                throw new Exception("Компонент CarView не найден на View объекте!");

            return carView;
        }

        private void RotateWheels(float value)
        {
            _carView.RotateWheels(new Vector3(0f, 0f, -value * _playerProfileModel.CurrentCarModel.Speed));
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(RotateWheels);
            _rightMove.UnSubscribeOnChange(RotateWheels);
        }
    }
}