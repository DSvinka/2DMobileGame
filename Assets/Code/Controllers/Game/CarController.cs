using System;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class CarController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/Car" };
        private readonly CarView _carView;

        public CarController()
        {
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