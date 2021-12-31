using System;
using System.Collections.Generic;
using Code.Configs.Items;
using Code.Models;
using Code.States;
using Code.Utils;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers.Garage
{
    public sealed class GarageController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath() { PathResource = "Prefabs/UI/GarageMenu" };
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly GarageView _garageView;

        public GarageController(PlayerProfileModel playerProfileModel, List<ItemConfig> itemConfigs, Transform spawnUIPosition)
        {
            _garageView = LoadView(spawnUIPosition);
            _garageView.Init(OnExitButton);
            _playerProfileModel = playerProfileModel;

            var inventoryController = new InventoryController(itemConfigs, _garageView);
            inventoryController.LoadInventory();
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