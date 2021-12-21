using Code.Analytics;
using Code.Properties;
using Code.States;

namespace Code.Models
{
    public sealed class PlayerProfileModel
    {
        public CarModel CurrentCarModel { get; private set; }
        public SubscribeProperty<GameState> CurrentGameState { get; }
        
        public IUnityPurchasingTools UnityPurchasingTools { get; }
        public IAnalyticsTools AnalyticsTools { get; }
        public IAdsShower AdsShower { get; }

        public PlayerProfileModel(float speed, IAdsShower unityAdsTools, IUnityPurchasingTools unityPurchasingTools)
        {
            CurrentGameState = new SubscribeProperty<GameState>();
            CurrentCarModel = new CarModel(speed);

            AnalyticsTools = new UnityAnalyticsTools();

            UnityPurchasingTools = unityPurchasingTools;
            AdsShower = unityAdsTools;
        }
    }
}