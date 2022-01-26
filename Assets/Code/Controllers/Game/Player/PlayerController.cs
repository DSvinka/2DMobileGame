using Code.Enums;
using Code.Models;
using UnityEngine;

namespace Code.Controllers.Game.Player
{
    public sealed class PlayerController: BaseController
    {
        private PlayerProfileModel _playerProfileModel;

        public PlayerController(InputModel inputModel, PlayerProfileModel playerProfileModel, Transform placeForUi, Camera camera)
        {
            _playerProfileModel = playerProfileModel;
            
            var enterGarageController = new PlayerEnterGarageController(inputModel, playerProfileModel);
            AddController(enterGarageController);
            
            var hudController = new PlayerHudController(inputModel, placeForUi, enterGarageController.GetGameObject());
            AddController(hudController);
            
            var carController = new PlayerCarController(inputModel, playerProfileModel, camera);
            AddController(carController);
            
            _playerProfileModel.CurrentCarModel.OnDeath += OnPlayerDeath;
        }

        protected override void OnDispose()
        {
            _playerProfileModel.CurrentCarModel.OnDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath(int id)
        {
            Debug.Log("Вы погибли!");
            _playerProfileModel.CurrentGameState.Value = GameState.Start;
        }
    }
}