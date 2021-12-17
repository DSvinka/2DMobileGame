using Code.Controllers.Game;
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
        private readonly Transform _placeForUi;
        private readonly PlayerProfileModel _playerProfileModel;
        
        public MainController(Transform placeForUi, PlayerProfileModel playerProfile)
        {
            _playerProfileModel = playerProfile;
            _placeForUi = placeForUi;
            OnChangeGameState(_playerProfileModel.CurrentGameState.Value);
            _playerProfileModel.CurrentGameState.SubscribeOnChange(OnChangeGameState);
        }

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            
            _playerProfileModel.CurrentGameState.UnSubscribeOnChange(OnChangeGameState);
            base.OnDispose();
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _playerProfileModel);
                    _gameController?.Dispose();
                    break;
                case GameState.Game:
                    _gameController = new GameController(_playerProfileModel);
                    _mainMenuController?.Dispose();
                    break;
                default:
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    break;
            }
        }
    }
}
