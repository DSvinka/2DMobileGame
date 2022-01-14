using System;
using Code.Interfaces.Properties;
using Code.Models;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game
{
    public sealed class HudController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/UI/Hud"};
        
        private PlayerProfileModel _playerProfileModel;
        private EnterGarageView _enterGarageView;
        private HudView _hudView;

        private readonly IReadOnlySubscribeProperty<float> _leftMove;
        private readonly IReadOnlySubscribeProperty<float> _rightMove;

        public HudController(Transform spawnUIPosition, EnterGarageView enterGarageView, IReadOnlySubscribeProperty<float> leftMove, IReadOnlySubscribeProperty<float> rightMove)
        {
            _hudView = LoadView(spawnUIPosition);
            
            _leftMove = leftMove;
            _rightMove = rightMove;

            _enterGarageView = enterGarageView;

            _leftMove.SubscribeOnChange(OnMoved);
            _rightMove.SubscribeOnChange(OnMoved);
        }

        public HudView GetGameObject()
        {
            return _hudView;
        }
        
        private HudView LoadView(Transform spawnUIPosition)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            
            if (!objView.TryGetComponent<HudView>(out var hudView))
                throw new Exception("Компонент HudView не найден на View объекте!");
            
            AddGameObject(objView);
            return hudView;
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(OnMoved);
            _rightMove.UnSubscribeOnChange(OnMoved);
        
            base.OnDispose();
        }
        private void OnMoved(float value)
        {
            _hudView.ChangeDistanceToGarage(Vector2.Distance(Vector2.zero, _enterGarageView.transform.position));
        }
    }
}