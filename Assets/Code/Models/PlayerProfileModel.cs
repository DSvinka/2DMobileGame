using Code.Properties;
using Code.States;

namespace Code.Models
{
    public sealed class PlayerProfileModel
    {
        public CarModel CurrentCarModel { get; }
        public SubscribeProperty<GameState> CurrentGameState { get; }

        public PlayerProfileModel(float speed)
        {
            CurrentGameState = new SubscribeProperty<GameState>();
            CurrentCarModel = new CarModel(speed);
        }
    }
}