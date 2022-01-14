using System.Linq;
using Code.Analytics;
using Code.Configs.Abilities;
using Code.Configs.Items;
using Code.Controllers;
using Code.Models;
using Code.States;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Code
{
    public sealed class Root : MonoBehaviour
    {
        [SerializeField] private Transform _uiPoint;
        [SerializeField] private UnityAdsTools _unityAdsTools;
        [SerializeField] private UnityPurchasingTools _unityPurchasingTools;

        [SerializeField] private ItemConfig[] _itemConfigs;
        [SerializeField] private AbilityItemConfig[] _abilityConfigs;
        
        private MainController _mainController;

        private void Awake()
        {
            var products = new ProductConf[]
            {
                new ProductConf("green_car", "Зелёная Машина", "Машина с зелёным цветом", 200, ProductType.Consumable)
            };
                
            var purchaseModel = new PurchaseModel(products);
            var playerProfile = new PlayerProfileModel(15f, _unityAdsTools, _unityPurchasingTools);
            
            playerProfile.CurrentGameState.Value = GameState.Start;
            
            _mainController = new MainController(_uiPoint, playerProfile, purchaseModel, _itemConfigs.ToList(), _abilityConfigs.ToList());
        }

        private void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
