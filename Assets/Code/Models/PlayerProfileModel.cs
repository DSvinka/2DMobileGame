using Code.Analytics;
using Code.Properties;
using Code.Repositories;
using Code.States;

namespace Code.Models
{
    public sealed class PlayerProfileModel
    {
        private float _speed;

        private CarModel _currentCarModel;
        private SavesRepository _savesRepository;

        private SubscribeProperty<GameState> _currentGameState;

        private IUnityPurchasingTools _unityPurchasingTools;
        private IAnalyticsTools _analyticsTools;
        private IAdsShower _adsShower;

        public float Speed => _speed;
        public CarModel CurrentCarModel => _currentCarModel;
        public SavesRepository SavesRepository => _savesRepository;

        public SubscribeProperty<GameState> CurrentGameState => _currentGameState;

        public IUnityPurchasingTools UnityPurchasingTools => _unityPurchasingTools;
        public IAnalyticsTools AnalyticsTools => _analyticsTools;
        public IAdsShower AdsShower => _adsShower;

        public PlayerProfileModel(float speed, IAdsShower unityAdsTools, IUnityPurchasingTools unityPurchasingTools)
        {
            Reset();
            
            _speed = speed;
            _savesRepository = new SavesRepository();
            _currentGameState = new SubscribeProperty<GameState>();

            _analyticsTools = new UnityAnalyticsTools();
            _unityPurchasingTools = unityPurchasingTools;
            _adsShower = unityAdsTools;
        }

        public void Reset()
        {
            // TODO: Нужно избавится от магических чисел, например сделать конфиг машинки.
            _currentCarModel = new CarModel(new CarModelConfig()
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