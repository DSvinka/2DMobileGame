using Code.Models;
using Code.Properties;
using Code.Utils;
using Code.Views;
using UnityEngine;

namespace Code.Controllers.Game
{
    public sealed class BackgroundController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/GameBackground"};
        private readonly SubscribeProperty<float> _moveUpdate;
        private readonly SubscribeProperty<float> _diff;

        private PlayerProfileModel _playerProfileModel;
        private TapeBackgroundView _view;
        
        public BackgroundController(InputModel inputModel, PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
            
            _view = LoadView();
            _moveUpdate = inputModel.MoveUpdate;
            _moveUpdate.SubscribeOnChange(Move);
            
            _diff = new SubscribeProperty<float>();

            _view.Init(_diff);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _moveUpdate.UnSubscribeOnChange(Move);
        }

        private TapeBackgroundView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);
        
            return objView.GetComponent<TapeBackgroundView>();
        }

        private void Move(float deltatime)
        {
            _diff.Value = _playerProfileModel.Speed * deltatime;
        }
    }
}

