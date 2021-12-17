using System;
using Code.Models;
using Code.Properties;
using Code.Utils;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    internal class InputController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/gyroscopeMove"};
        private readonly BaseInputView _baseInputView;
        
        public InputController(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, CarModel car)
        {
            _baseInputView = LoadView();
            _baseInputView.Init(leftMove, rightMove, car.Speed);
        }

        private BaseInputView LoadView()
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objectView);
            
            if (!objectView.TryGetComponent<BaseInputView>(out var baseInputView))
                throw new Exception("Компонент CarView не найден на View объекте!");
            
            return baseInputView;
        }
    }

}