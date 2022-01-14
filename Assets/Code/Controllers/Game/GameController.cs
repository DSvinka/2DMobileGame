using System.Collections.Generic;
using Code.Configs.Abilities;
using Code.Configs.Items;
using Code.Models;
using Code.Properties;
using UnityEngine;

namespace Code.Controllers.Game
{
    public class GameController : BaseController
    {
        public GameController(PlayerProfileModel playerProfileModel, List<AbilityItemConfig> abilityConfigs, Transform placeForUi)
        {
            var leftMoveDiff = new SubscribeProperty<float>();
            var rightMoveDiff = new SubscribeProperty<float>();
        
            var backgroundController = new BackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(backgroundController);

            var enterGarageController = new EnterGarageController(playerProfileModel, leftMoveDiff, rightMoveDiff);
            AddController(enterGarageController);

            var hudController = new HudController(placeForUi, enterGarageController.GetGameObject(), leftMoveDiff, rightMoveDiff);
            AddController(hudController);
        
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, playerProfileModel.CurrentCarModel);
            AddController(inputGameController);
            
            var carController = new CarController(leftMoveDiff, rightMoveDiff, playerProfileModel);
            AddController(carController);

            var abilityController = new AbilityController(abilityConfigs, hudController.GetGameObject());
            AddController(abilityController);
        }
    }
}

