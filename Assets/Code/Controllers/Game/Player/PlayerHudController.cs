using System;
using Code.Models;
using Code.Properties;
using Code.Utils;
using Code.Views;
using Code.Views.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game.Player
{
    public sealed class PlayerHudController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/UI/Hud"};

        private SubscribeProperty<float> _moveUpdate;
        private PlayerProfileModel _playerProfileModel;
        private EnterGarageView _enterGarageView;
        private HudView _hudView;

        public PlayerHudController(InputModel inputModel, Transform spawnUIPosition, EnterGarageView enterGarageView)
        {
            _hudView = LoadView(spawnUIPosition);
            _moveUpdate = inputModel.MoveUpdate;
            _enterGarageView = enterGarageView;
            
            _moveUpdate.SubscribeOnChange(OnMoved);
        }
        
        protected override void OnDispose()
        {
            base.OnDispose();
            _moveUpdate.UnSubscribeOnChange(OnMoved);
        }

        private HudView LoadView(Transform spawnUIPosition)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            
            if (!objView.TryGetComponent<HudView>(out var hudView))
                throw new Exception("Компонент HudView не найден на View объекте!");
            
            AddGameObject(objView);
            return hudView;
        }
        
        private void OnMoved(float deltatime)
        {
            _hudView.ChangeDistanceToGarage((int) Vector2.Distance(Vector2.zero, _enterGarageView.transform.position));
        }
    }
}