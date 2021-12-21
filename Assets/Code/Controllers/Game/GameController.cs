using Code.Models;
using Code.Properties;

namespace Code.Controllers.Game
{
    public class GameController : BaseController
    {
        public GameController(PlayerProfileModel playerProfileModel)
        {
            var leftMoveDiff = new SubscribeProperty<float>();
            var rightMoveDiff = new SubscribeProperty<float>();
        
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);
        
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, playerProfileModel.CurrentCarModel);
            AddController(inputGameController);
            
            var carController = new CarController(playerProfileModel);
            AddController(carController);
        }
    }
}

