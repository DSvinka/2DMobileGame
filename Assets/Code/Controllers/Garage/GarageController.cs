using System;
using Code.Configs;
using Code.Enums;
using Code.Models;
using Code.Utils;
using Code.Views.Garage;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Garage
{
    public sealed class GarageController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/UI/Menus/GarageMenu" };
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly GarageView _garageView;

        public GarageController(Transform spawnUIPosition, PlayerProfileModel playerProfileModel, DataSources dataSources)
        {
            _garageView = LoadView(spawnUIPosition);
            _garageView.Init(OnExitButton);
            _playerProfileModel = playerProfileModel;

            var inventoryController = new UpgradesController(_garageView, playerProfileModel, dataSources);
            inventoryController.Load();
            AddController(inventoryController);
        }
        
        private GarageView LoadView(Transform spawnUIPosition)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), spawnUIPosition, false);
            AddGameObject(objectView);

            if (!objectView.TryGetComponent<GarageView>(out var garageView))
                throw new Exception("Компонент GarageView не найден на View объекте!");

            return garageView;
        }

        private void OnExitButton()
        {
            _playerProfileModel.CurrentGameState.Value = GameState.Game;
        }
    }
}