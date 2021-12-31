using System;
using Code.Interfaces.Properties;
using Code.Models;
using Code.Properties;
using Code.States;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class EnterGarageController : BaseController
    {
        private const float DistanceForEnter = 5f;
        private const float SpawnXPosition = 300f;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/GarageView"};
        
        private readonly SubscribeProperty<float> _diff;
        private readonly IReadOnlySubscribeProperty<float> _leftMove;
        private readonly IReadOnlySubscribeProperty<float> _rightMove;

        private PlayerProfileModel _playerProfileModel;
        private EnterGarageView _enterGarageView;
        
        public EnterGarageController(PlayerProfileModel playerProfileModel, IReadOnlySubscribeProperty<float> leftMove, IReadOnlySubscribeProperty<float> rightMove)
        {
            _enterGarageView = LoadView();
            _diff = new SubscribeProperty<float>();
        
            _leftMove = leftMove;
            _rightMove = rightMove;
        
            _enterGarageView.Init(_diff);
        
            _leftMove.SubscribeOnChange(Move);
            _rightMove.SubscribeOnChange(Move);

            _playerProfileModel = playerProfileModel;
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(Move);
            _rightMove.UnSubscribeOnChange(Move);
        
            base.OnDispose();
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

        private void Move(float value)
        {
            _diff.Value = value;
            
            var distance = Vector2.Distance(Vector2.zero, _enterGarageView.transform.position);
            if (distance <= DistanceForEnter)
            {
                _playerProfileModel.CurrentGameState.Value = GameState.Garage;
            }
        }
    }
}