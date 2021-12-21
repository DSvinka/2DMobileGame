using System;
using Code.Models;
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

        public CarController(PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
            
            _viewPath = _playerProfileModel.CurrentCarModel.ResourcePath;
            _carView = LoadView();
        }

        private CarView LoadView()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<CarView>(out var carView))
                throw new Exception("Компонент CarView не найден на View объекте!");

            return carView;
        }

        public GameObject GetViewObject()
        {
            return _carView.gameObject;
        }
    }
}