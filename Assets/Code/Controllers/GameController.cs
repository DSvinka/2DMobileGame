namespace Code.Controllers
{
    public sealed class GameController: BaseController
    {
        public GameController()
        {
            var carController = new CarController();
            AddController(carController);
        }
    }
}