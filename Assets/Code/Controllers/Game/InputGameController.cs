using System;
using Code.Models;
using Code.Properties;
using Code.Utils;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class InputGameController : BaseController
    {
        public InputGameController(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, CarModel car)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, car.Speed);
        }

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/InputView"};
        private BaseInputView _view;

        private BaseInputView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);

            if (!objView.TryGetComponent<BaseInputView>(out var inputView))
                throw new Exception("Компонент BaseInputView не найден в View");
            
            return inputView;
        }
    }
}

