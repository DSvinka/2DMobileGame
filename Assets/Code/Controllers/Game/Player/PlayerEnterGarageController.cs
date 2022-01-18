using System;
using Code.Models;
using Code.Properties;
using Code.States;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game.Player
{
    public sealed class PlayerEnterGarageController : BaseController
    {
        private const float DistanceForEnter = 5f;
        private const float SpawnXPosition = 300f;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/GarageView"};
        private readonly SubscribeProperty<float> _moveUpdate;
        private readonly SubscribeProperty<float> _diff;

        private PlayerProfileModel _playerProfileModel;
        private EnterGarageView _enterGarageView;
        
        public PlayerEnterGarageController(InputModel inputModel, PlayerProfileModel playerProfileModel)
        {
            _enterGarageView = LoadView();
            _diff = new SubscribeProperty<float>();
            
            _moveUpdate = inputModel.MoveUpdate;
            _moveUpdate.SubscribeOnChange(Move);
            
            _enterGarageView.Init(_diff);

            _playerProfileModel = playerProfileModel;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _moveUpdate.SubscribeOnChange(Move);
        }

        public EnterGarageView GetGameObject()
        {
            return _enterGarageView;
        }

        private EnterGarageView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            if (!objView.TryGetComponent<EnterGarageView>(out var enterGarageView))
                throw new Exception("Компонент EnterGarageView не найден на View объекте!");
            
            objView.transform.position = new Vector3(SpawnXPosition, objView.transform.position.y, 0f);
        
            AddGameObject(objView);
            return enterGarageView;
        }

        private void Move(float deltatime)
        {
            _diff.Value = _playerProfileModel.Speed * deltatime;
            
            var distance = Vector2.Distance(Vector2.zero, _enterGarageView.transform.position);
            if (distance <= DistanceForEnter)
            {
                _playerProfileModel.CurrentGameState.Value = GameState.Garage;
            }
        }
    }
}