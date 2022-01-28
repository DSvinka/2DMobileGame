using System;
using Code.Models;
using Code.Utils;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class InputGameController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/InputView"};
        private BaseInputView _view;
        
        public InputGameController(InputModel inputModel, PlayerProfileModel playerProfileModel)
        {
            _view = LoadView();
            _view.Init(inputModel, playerProfileModel.Speed);
        }

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

