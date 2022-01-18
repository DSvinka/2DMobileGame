using Code.Analytics;
using Code.Properties;
using Code.States;

namespace Code.Models
{
    public sealed class PlayerProfileModel
    {
        public float Speed { get; }
        public CarModel CurrentCarModel { get; private set; }
        public SubscribeProperty<GameState> CurrentGameState { get; }
        
        public IUnityPurchasingTools UnityPurchasingTools { get; }
        public IAnalyticsTools AnalyticsTools { get; }
        public IAdsShower AdsShower { get; }

        public PlayerProfileModel(float speed, IAdsShower unityAdsTools, IUnityPurchasingTools unityPurchasingTools)
        {
            Reset();
            Speed = speed;
            CurrentGameState = new SubscribeProperty<GameState>();

            AnalyticsTools = new UnityAnalyticsTools();

            UnityPurchasingTools = unityPurchasingTools;
            AdsShower = unityAdsTools;
        }

        public void Reset()
        {
            // TODO: Нужно избавится от магических чисел, например сделать конфиг машинки.
            CurrentCarModel = new CarModel(new CarModelConfig()
            {
                EntityView = null,
                
                MaxHealth = 100f,
                ShotRate = 1f,
                
                BulletLifeTime = 20f,
                BulletShotForce = 15f,
                BulletDamage = 10f,
            });
        }
    }
}