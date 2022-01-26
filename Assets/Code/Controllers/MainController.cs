using Code.Configs;
using Code.Configs.Items;
using Code.Controllers.Game;
using Code.Controllers.Garage;
using Code.Controllers.Start;
using Code.Enums;
using Code.Models;
using UnityEngine;

namespace Code.Controllers
{
    public sealed class MainController : BaseController
    {
        private PlayerCurrencyController _playerCurrencyController;
        private MainMenuController _mainMenuController;
        private GarageController _garageController;
        private GameController _gameController;
        
        private readonly Camera _camera;
        private readonly Transform _placeForUi;
        private readonly DataSources _dataSources;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly PurchaseModel _purchaseModel;

        // Вынести параметры в отдельный struct.
        public MainController(Transform placeForUi, PlayerProfileModel playerProfileModel, DataSources dataSources, Camera camera, PurchaseModel purchaseModel)
        {
            _dataSources = dataSources;
            _purchaseModel = purchaseModel;
            _playerProfileModel = playerProfileModel;
            _placeForUi = placeForUi;
            _camera = camera;

            _playerCurrencyController = new PlayerCurrencyController(placeForUi, playerProfileModel);
            AddController(_playerCurrencyController);
            
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
                    _mainMenuController = new MainMenuController(_placeForUi, _playerProfileModel, _dataSources, _purchaseModel);
                    _gameController?.Dispose();
                    _garageController?.Dispose();
                    break;
                
                case GameState.Garage:
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    _garageController = new GarageController(_placeForUi, _playerProfileModel, _dataSources);
                    break;
                
                case GameState.Game:
                    _playerProfileModel.Reset();
                    
                    _gameController = new GameController(_placeForUi, _playerProfileModel, _dataSources, _camera);
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
