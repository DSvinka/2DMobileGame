using System.Collections.Generic;
using Code.Configs.Items;
using Code.Controllers.Game;
using Code.Controllers.Garage;
using Code.Controllers.Start;
using Code.Models;
using Code.States;
using UnityEngine;

namespace Code.Controllers
{
    public sealed class MainController : BaseController
    {
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private GarageController _garageController;

        private readonly Camera _camera;
        private readonly Transform _placeForUi;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly PurchaseModel _purchaseModel;
        private readonly List<ItemConfig> _itemConfigs;

        // Вынести параметры в отдельный struct.
        public MainController(Transform placeForUi, Camera camera, PlayerProfileModel playerProfile, PurchaseModel purchaseModel, List<ItemConfig> itemConfigs)
        {
            _itemConfigs = itemConfigs;
            _purchaseModel = purchaseModel;
            _playerProfileModel = playerProfile;
            _placeForUi = placeForUi;
            _camera = camera;
            
            OnChangeGameState(_playerProfileModel.CurrentGameState.Value);
            _playerProfileModel.CurrentGameState.SubscribeOnChange(OnChangeGameState);
        }

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _garageController?.Dispose();
            
            _playerProfileModel.CurrentGameState.UnSubscribeOnChange(OnChangeGameState);
            base.OnDispose();
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _playerProfileModel, _purchaseModel);
                    _gameController?.Dispose();
                    _garageController?.Dispose();
                    break;
                case GameState.Garage:
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    _garageController = new GarageController(_playerProfileModel, _itemConfigs, _placeForUi);
                    break;
                case GameState.Game:
                    _playerProfileModel.Reset();
                    
                    _gameController = new GameController(_playerProfileModel, _camera, _placeForUi);
                    _mainMenuController?.Dispose();
                    _garageController?.Dispose();
                    break;
                default:
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    _garageController?.Dispose();
                    break;
            }
        }
    }
}
