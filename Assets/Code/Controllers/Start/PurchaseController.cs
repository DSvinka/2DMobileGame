using Code.Models;
using Code.Utils;
using Code.Views;
using Code.Views.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Code.Controllers.Start
{
    public sealed class PurchaseController: BaseController
    {
        private PurchaseMenuView _purchaseMenuView;
        
        private PlayerProfileModel _playerProfileModel;
        private PurchaseModel _purchaseModel;
        
        public PurchaseController(PurchaseMenuView purchaseMenuView, PlayerProfileModel playerProfileModel, PurchaseModel purchaseModel)
        {
            _purchaseModel = purchaseModel;
            _playerProfileModel = playerProfileModel;
            _purchaseMenuView = purchaseMenuView;
            
            _purchaseMenuView.Init(CloseMenu);
            _playerProfileModel.UnityPurchasingTools.OnPurchase += OnPurchase;

            InitPurchasingTools();
            InitPurchasingMenu();
            
            AddGameObject(_purchaseMenuView.gameObject);
        }

        // TODO: Добавить создание карточек для продуктов
        private void InitPurchasingMenu()
        {
            for (var i = 0; i < _purchaseModel.Products.Length; i++)
            {
                if (i >= _purchaseMenuView.ProductViews.Length) break;
                
                var product = _purchaseModel.Products[i];
                _purchaseMenuView.ProductViews[i].Init(delegate { DonateBuy(product.ID); }, product.Title, product.Description, product.PriceRubles, product.ID);
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
        
        public void OpenMenu()
        {
            _purchaseMenuView.transform.localScale = Vector3.zero;
            
            _purchaseMenuView.gameObject.SetActive(true);
            _purchaseMenuView.transform.DOScale(1f, 0.5f);
        }

        private void CloseMenu()
        {
            _purchaseMenuView.transform.DOScale(0f, 0.5f).OnComplete(() =>
            {
                _purchaseMenuView.gameObject.SetActive(false);
            });
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