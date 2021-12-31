using Code.Models;
using Code.Utils;
using Code.Views;
using UnityEngine.Purchasing;

namespace Code.Controllers.Start
{
    public sealed class PurchaseController: BaseController
    {
        private ProductsMenuView _productsMenuView;
        
        private PlayerProfileModel _playerProfileModel;
        private PurchaseModel _purchaseModel;
        
        public PurchaseController(ProductsMenuView productsMenuView, PlayerProfileModel playerProfileModel, PurchaseModel purchaseModel)
        {
            _purchaseModel = purchaseModel;
            _playerProfileModel = playerProfileModel;
            _productsMenuView = productsMenuView;
            
            _productsMenuView.Init(CloseMenu);
            _playerProfileModel.UnityPurchasingTools.OnPurchase += OnPurchase;

            InitPurchasingTools();
            InitPurchasingMenu();
            
            AddGameObject(_productsMenuView.gameObject);
        }

        // TODO: Добавить создание карточек для продуктов
        private void InitPurchasingMenu()
        {
            for (var i = 0; i < _purchaseModel.Products.Length; i++)
            {
                if (i >= _productsMenuView.ProductViews.Length) break;
                
                var product = _purchaseModel.Products[i];
                _productsMenuView.ProductViews[i].Init(delegate { DonateBuy(product.ID); }, product.Title, product.Description, product.PriceRubles, product.ID);
            }
        }

        private void InitPurchasingTools()
        {
            var builder = _playerProfileModel.UnityPurchasingTools.GetBuilder();

            foreach (var product in _purchaseModel.Products)
            {
                builder.AddProduct(product.ID, product.Type);
            }

            _playerProfileModel.UnityPurchasingTools.Init(builder);
        }
        
        protected override void OnDispose()
        {
            _playerProfileModel.UnityPurchasingTools.OnPurchase -= OnPurchase;
        }

        private void DonateBuy(string productID)
        {
            _playerProfileModel.UnityPurchasingTools.BuyProduct(productID);
        }

        private void CloseMenu()
        {
            _productsMenuView.gameObject.SetActive(false);
        }
        
        private void OnPurchase(Product product)
        {
            if (product.definition.id == "green_car")
            {
                _playerProfileModel.CurrentCarModel.ResourcePath = new ResourcePath() {PathResource = "Prefabs/Cars/CarGreen"};
            }
        }
    }
}