using Code.Configs;
using Code.Controllers.Game.Player;
using Code.Models;
using Code.Properties;
using UnityEngine;

namespace Code.Controllers.Game
{
    public class GameController : BaseController
    {
        public GameController(Transform placeForUi, PlayerProfileModel playerProfileModel, DataSources dataSources, Camera camera)
        {
            var touchPosition = new SubscribeProperty<Vector2>();
            var moveUpdate = new SubscribeProperty<float>();

            var inputModel = new InputModel(touchPosition, moveUpdate);
        
            var backgroundController = new BackgroundController(inputModel, playerProfileModel);
            AddController(backgroundController);

            var enemiesController = new EnemiesController(inputModel, playerProfileModel, dataSources.EnemiesDataSource);
            AddController(enemiesController);

            var inputGameController = new InputGameController(inputModel, playerProfileModel);
            AddController(inputGameController);

            var playerController = new PlayerController(inputModel, playerProfileModel, placeForUi, camera);
            AddController(playerController);
        }
    }
}

