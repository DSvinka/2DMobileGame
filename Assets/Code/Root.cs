using System.Linq;
using Code.Analytics;
using Code.Configs;
using Code.Configs.Items;
using Code.Configs.Upgrades;
using Code.Controllers;
using Code.Models;
using Code.States;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Code
{
    public sealed class Root : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _uiPoint;
        [SerializeField] private UnityAdsTools _unityAdsTools;
        [SerializeField] private UnityPurchasingTools _unityPurchasingTools;
        
        [SerializeField] private DataSources _dataSources;

        private MainController _mainController;

        private void Awake()
        {
            var products = new ProductConf[]
            {
                new ProductConf("green_car", "Зелёная Машина", "Машина с зелёным цветом", 200, ProductType.Consumable)
            };
                
            var purchaseModel = new PurchaseModel(products);
            var playerProfileModel = new PlayerProfileModel(15f, _unityAdsTools, _unityPurchasingTools);
            
            playerProfileModel.CurrentGameState.Value = GameState.Start;
            
            _mainController = new MainController(_uiPoint, playerProfileModel, _dataSources, _camera, purchaseModel);
        }

        private void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
