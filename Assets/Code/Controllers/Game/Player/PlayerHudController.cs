using System;
using System.Collections.ObjectModel;
using Code.Models;
using Code.Properties;
using Code.UnityUtils;
using Code.Utils;
using Code.Views;
using Code.Views.UI;
using UnityEngine;
using UnityEngine.Localization.Tables;
using Object = UnityEngine.Object;

namespace Code.Controllers.Game.Player
{
    public sealed class PlayerHudController: BaseController
    {
        private readonly string LocalizationTable = "Game";
        private readonly string LocalizationGarageDistanceText = "garage_distance_text";
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/UI/Hud"};

        private readonly UnityLocalizationTools _unityLocalizationTools;
        private readonly SubscribeProperty<float> _moveUpdate;
        private readonly EnterGarageView _enterGarageView;
        private readonly HudView _hudView;

        public PlayerHudController(InputModel inputModel, Transform spawnUIPosition, EnterGarageView enterGarageView)
        {
            _hudView = LoadView(spawnUIPosition);
            _moveUpdate = inputModel.MoveUpdate;
            _enterGarageView = enterGarageView;
            
            _unityLocalizationTools = new UnityLocalizationTools(LocalizationTable);
            AddController(_unityLocalizationTools);

            _moveUpdate.SubscribeOnChange(OnMoved);
        }
        
        protected override void OnDispose()
        {
            base.OnDispose();
            _moveUpdate.UnSubscribeOnChange(OnMoved);
        }

        private HudView LoadView(Transform spawnUIPosition)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), spawnUIPosition);
            
            if (!objView.TryGetComponent<HudView>(out var hudView))
                throw new Exception("Компонент HudView не найден на View объекте!");
            
            AddGameObject(objView);
            return hudView;
        }
        
        private void OnMoved(float deltaTime)
        {
            var distance = (int) Vector2.Distance(Vector2.zero, _enterGarageView.transform.position);
            _hudView.ChangeDistanceToGarageText(_unityLocalizationTools.GetLocalizedString(LocalizationGarageDistanceText, distance));
        }
    }
}