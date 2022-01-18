using Code.Models;
using UnityEngine;

namespace Code.Controllers.Game.Player
{
    public sealed class PlayerController: BaseController
    {
        public PlayerController(InputModel inputModel, PlayerProfileModel playerProfileModel, Transform placeForUi, Camera camera)
        {
            var enterGarageController = new PlayerEnterGarageController(inputModel, playerProfileModel);
            AddController(enterGarageController);
            
            var hudController = new PlayerHudController(inputModel, placeForUi, enterGarageController.GetGameObject());
            AddController(hudController);
            
            var carController = new PlayerCarController(inputModel, playerProfileModel, camera);
            AddController(carController);
        }
    }
}