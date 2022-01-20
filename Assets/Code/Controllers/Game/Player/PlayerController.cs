using Code.Models;
using Code.States;
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
            _playerProfileModel.CurrentCarModel.OnDamage += OnPlayerDamage;
        }
        
        private void OnPlayerDeath(int id)
        {
            Debug.Log("Вы погибли!");
            _playerProfileModel.CurrentGameState.Value = GameState.Start;
        }
        
        private void OnPlayerDamage(int id, float damage)
        {
            var carModel = _playerProfileModel.CurrentCarModel;
            carModel.EntityView.HealthText.text = $"HP: {carModel.Health}";
        }
    }
}